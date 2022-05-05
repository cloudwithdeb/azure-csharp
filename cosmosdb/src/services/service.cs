using Microsoft.Azure.Cosmos;
using UsersInterfaceNSP;
using UsersModelNSP;

namespace UsersServiceNSP
{
    public class UserService : IUsersMthodsInterface
    {
        // Define Default variables variables
        private readonly string _primary_connection_str;
        private readonly string _partition_key;
        private readonly  CosmosClient _client;

        // Create constructore to initialize values
        public UserService()
        {
            _primary_connection_str = "AccountEndpoint=https://cosmosdb-testapp.documents.azure.com:443/;AccountKey=d12jH0g0E15v30w98jRwzfXOPTZqFFVP4QDOQ2n7UbDqWBuD35pe431zyANX5CLeLz8u07bxRmRlSunsoKyXvw==;";
            _partition_key = "/orgid";

              // Initialize cosmosDB connection Client
            _client = new CosmosClient(_primary_connection_str);

        }

        // Create azure database client
        public string CreateDatabaseAndCollection(DataBaseCol config)
        {
            // Create Database with provided details
            _client.CreateDatabaseAsync(config.dbname).GetAwaiter().GetResult();

            // Get Database name and Create collection
            Database _database = _client.GetDatabase(config.dbname);
            _database.CreateContainerAsync(config.contname, _partition_key).GetAwaiter().GetResult();

            // Return response to client
            return "Database and container created successfully. Thank you!";
        }

        public string AddUser(UsersModel user)
        {
            throw new NotImplementedException();
        }

        public string DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<UsersModel> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UsersModel GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public string UpdateUsers(int id, UsersModel user)
        {
            throw new NotImplementedException();
        }
    }
}