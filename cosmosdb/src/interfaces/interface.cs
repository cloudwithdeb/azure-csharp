using UsersModelNSP;

namespace UsersInterfaceNSP
{

    //Method interface to impliment
    public interface IUsersMthodsInterface
    {
        public string AddUser(UsersModel user);
        public string AddMultitpleUsers(UsersModel[] users);
        public List<UsersModel> GetUserFromCosmosDBUsingId(string id);
        public UsersModel GetUserById(int id);
        public List<UsersModel> GetAllUsers();
        public string UpdateUsers(string id, UsersModel user);
        public string DeleteUser(string id,  int usersid);
        public string CreateDatabaseAndCollection(DataBaseCol config);
    }

}