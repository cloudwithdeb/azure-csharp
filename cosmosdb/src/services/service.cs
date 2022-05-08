using Microsoft.Azure.Cosmos;
using UsersInterfaceNSP;
using UsersModelNSP;
using UsersRepositoryNSP;

namespace UsersServiceNSP
{
    public class UserService : IUsersMthodsInterface
    {
        // Define Default variables variables
        private readonly string _primary_connection_str;
        private readonly string _partition_key;
        private readonly  CosmosClient _client;
        private readonly string _database_name;
        private readonly string _container_name;

        // Create constructore to initialize values
        public UserService()
        {
            _primary_connection_str = "AccountEndpoint=https://azdemoscosmosdb.documents.azure.com:443/;AccountKey=LZRU29U9zNIv43XwaH9rqk5rDATXLDMBtznUUUdBYUygLMrUuxYCJce7PEiZGziM1aKL5Jx1wUknGeIvdDUPNg==;";
            _partition_key = "/usersid";
            _database_name = "Gtuc";
            _container_name = "Students";

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

        // Add A new item to cosmosDB
        public string AddUser(UsersModel user)
        {
           var getContainer =  _client.GetContainer(_database_name, _container_name);
           getContainer.CreateItemAsync<UsersModel>(user).GetAwaiter().GetResult();
           return "user created successfully. Thank you!";

        }

        public string AddMultitpleUsers(UsersModel[] users)
        {
            Container getContainer =  _client.GetContainer(_database_name, _container_name);

            List<Task> _task = new List<Task>();

            foreach(UsersModel _user in users){
                _task.Add(getContainer.CreateItemAsync<UsersModel>(_user, new PartitionKey(_user.usersid)));
            }
            
            Task.WhenAll(_task).GetAwaiter().GetResult();
            return "Bulk user added successfully. Thank you!";
        }

        public List<UsersModel> GetUserFromCosmosDBUsingId(string id)
        {
            List<UsersModel> usr = new List<UsersModel>();

            Container _get_container =  _client.GetContainer(_database_name, _container_name);
            var _query = $"SELECT * FROM c WHERE c.id = '{id}'";
            QueryDefinition _definition = new QueryDefinition(_query);
            FeedIterator<UsersModel> _iterator = _get_container.GetItemQueryIterator<UsersModel>(_definition);

            while(_iterator.HasMoreResults)
            {
                FeedResponse<UsersModel> _response = _iterator.ReadNextAsync().GetAwaiter().GetResult();
                foreach(UsersModel usersmodel in _response)
                {
                    usr.Add(
                        new() {
                            Id=usersmodel.Id,
                            usersid=usersmodel.usersid,
                            username=usersmodel.username,
                            details=usersmodel.details,
                            others=usersmodel.others
                        }
                    );
                }
            }

            return usr;
        }

        public string DeleteUser(string id, int usersid)
        {
            Container _get_container =  _client.GetContainer(_database_name, _container_name);
            List<UsersModel> usr = GetUserFromCosmosDBUsingId(id);

            if(usr.Count > 0){
                _get_container.DeleteItemAsync<UsersModel>(id, new PartitionKey(usersid)).GetAwaiter().GetResult();
                return "User deleted successfully. Thank you!";
            }else{
                return "User does not exists";
            }
            
        }

        public List<UsersModel> GetAllUsers()
        {
            var users = UsersRepository.usr;
            return users;
        }

        public UsersModel GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public string UpdateUsers(string id, UsersModel user)
        {
        
        Container _get_container =  _client.GetContainer(_database_name, _container_name);
        List<UsersModel> usr = GetUserFromCosmosDBUsingId(id);

        //    Check if user exists or not
        if(usr.Count > 0){

            usr[0].username = user.username;
            _get_container.ReplaceItemAsync<UsersModel>(usr[0], id, new PartitionKey(user.usersid)).GetAwaiter().GetResult();
            return "User updated successfully. Thank you";

        }else{
            return "User does not exists";
        }
    }

        public string DeleteUser(int id, string usersid)
        {
            throw new NotImplementedException();
        }
    }
}