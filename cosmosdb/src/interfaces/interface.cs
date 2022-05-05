using UsersModelNSP;

namespace UsersInterfaceNSP
{

    //User model interface
    public interface IUsersModelInterface
    {
        public int Id {get; set;}
        public string? username {get; set;}
        public double? amount {get; set;}
        public string? email {get; set;}
        public string? password {get; set;}
    }

    //Method interface to impliment
    public interface IUsersMthodsInterface
    {
        public string AddUser(UsersModel user);
        public UsersModel GetUserById(int id);
        public List<UsersModel> GetAllUsers();
        public string UpdateUsers(int id, UsersModel user);
        public string DeleteUser(int id);
        public string CreateDatabaseAndCollection(DataBaseCol config);
    }

}