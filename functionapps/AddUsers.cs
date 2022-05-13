using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using UsersDataSetNamespace;
using System.Data.SqlClient;
using System.Data;

namespace functionapps
{
    public static class demoapp
    {
        [FunctionName("AddUsers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            
            // Get data from client from request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            // Get data from client from query and headers
            // string address = req.Query["address"];
            // string location = req.Headers["location"];
            
            // Get List of available users in database
             List<UsersDataSet> _lst = new List<UsersDataSet>();
            string ConnectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_ConnectionStringName");
            
            string _connection_string = ConnectionString;
            string GetAllUsers_Statement = "SELECT * FROM users";

            SqlConnection _connection = new SqlConnection(_connection_string);
            _connection.Open();

            SqlCommand _sqlcommand = new SqlCommand(GetAllUsers_Statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    UsersDataSet _users_dataset = new UsersDataSet()
                    {
                        Id = _reader.GetInt32(0),
                        username = _reader.GetString(1),
                        amount = _reader.GetDouble(2),
                        email = _reader.GetString(3),
                        loc = _reader.GetString(4)
                    };

                    _lst.Add(_users_dataset);
                }
            }

            // Add user to SQL Database
            int userID = _lst.Count + 1;
            string insert_statement = "INSERT INTO users(id, username, amount, email, loc) VALUES(@param1,@param2,@param3,@param4,@param5)";
      
            using(SqlCommand _command= new SqlCommand(insert_statement,_connection))
            {
                _command.Parameters.Add("@param1",SqlDbType.Int).Value= userID;
                _command.Parameters.Add("@param2", SqlDbType.VarChar, 1000).Value = data.username;
                _command.Parameters.Add("@param3", SqlDbType.Decimal).Value = (double)data.amount;
                _command.Parameters.Add("@param4", SqlDbType.VarChar).Value = data.email;
                _command.Parameters.Add("@param5", SqlDbType.VarChar).Value = data.loc;
                _command.CommandType = CommandType.Text;
                _command.ExecuteNonQuery();
            }

            return new OkObjectResult("User added successfully. Thank you!");
        }
    }
}
