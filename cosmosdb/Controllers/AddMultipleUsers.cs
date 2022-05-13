using Microsoft.AspNetCore.Mvc;
using UsersInterfaceNSP;
using UsersModelNSP;

namespace AddMultipleUsersNSP
{
    [ApiController]
    [Route("[controller]")]
    public class AddMultipleUsers : ControllerBase
    {
        private readonly IUsersMthodsInterface _svc;
        
        public AddMultipleUsers(IUsersMthodsInterface svc)
        {
            _svc = svc;
        }
        
        [HttpPost]
        public ActionResult AddMultipleUser(UsersModel[] users)
        {
            var results = _svc.AddMultitpleUsers(users);
            return Ok(results);
        }
    }
}