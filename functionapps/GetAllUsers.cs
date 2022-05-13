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

namespace functionapps
{
    public static class GetAllUsers
    {
        [FunctionName("GetAllUsers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            
            List<UsersDataSet> _lst = new List<UsersDataSet>();
            string ConnectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_ConnectionStringName");

            string _connection_string = ConnectionString;
            string _statement = "SELECT * FROM users";

            // We first establish a connection to the database
            SqlConnection _connection = new SqlConnection(_connection_string);
            _connection.Open();

               SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

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
            _connection.Close();      

            return new OkObjectResult(_lst);
        }
    }
}
