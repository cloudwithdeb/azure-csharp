using Microsoft.AspNetCore.Mvc;
using UsersInterfaceNSP;
using UsersModelNSP;

namespace AddUsersNSP
{
    [ApiController]
    [Route("[controller]")]
    public class AddUser : ControllerBase
    {
        private readonly IUsersMthodsInterface _svc;
        
        public AddUser(IUsersMthodsInterface svc)
        {
            _svc = svc;
        }

        [HttpPost]
        public ActionResult AddUsers(UsersModel user)
        {
            var results = _svc.AddUser(user);
            return Ok(results);
        }
    }
}