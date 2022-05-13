using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using UsersInterfaceNSP;

namespace UsersModelNSP
{
    public class Details
    {
        public string? email {get; set;}
        public string? location {get; set;}
        public int? age {get; set;}
    }
    public class Others
    {
        public string? mom {get; set;}
        public string? dad {get; set;}
    }
    public class UsersModel
    {
        [JsonProperty("id")]
        public string? Id {get; set;}
        public int usersid { get; set; }
        public string? username  { get; set; }
        public Details? details {get; set;}
        public List<Others>? others {get; set;}
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