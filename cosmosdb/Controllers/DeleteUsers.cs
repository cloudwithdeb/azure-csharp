using Microsoft.AspNetCore.Mvc;
using UsersInterfaceNSP;
using UsersModelNSP;

namespace DeleteUserNSP
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteUser : ControllerBase
    {
        // Define variable for interface
        private readonly IUsersMthodsInterface _svc;

        // Define constructor
        public DeleteUser(IUsersMthodsInterface svc)
        {
            _svc = svc;
        }

        // Define constructor
        [HttpDelete]
        public ActionResult DeleteAUser(string id, int userid)
        {
            var results = _svc.DeleteUser(id, userid);
            return Ok(results);
        }
    }
}