using System.ComponentModel.DataAnnotations;
using UsersInterfaceNSP;

namespace UsersModelNSP
{
    public class UsersModel : IUsersModelInterface
    {
        public int Id { get; set; }
        public string? username  { get; set; }
        public double? amount  { get; set; }
        public string? email { get; set; }
        public string? password  { get; set; }
    }

    //Database and collection model
    public class DataBaseCol
    {
        [Required]
        public string? dbname {get; set;}

        [Required]
        public string? contname {get; set;}
    }

}