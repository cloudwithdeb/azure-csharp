using Microsoft.AspNetCore.Mvc;
using UsersInterfaceNSP;

namespace GetALlUsersNSP
{
    [ApiController]
    [Route("[controller]")]
    public class GetAllUsers : ControllerBase
    {
        private readonly IUsersMthodsInterface _svc;

        public GetAllUsers(IUsersMthodsInterface svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public ActionResult GetAllUser()
        {
            var results = _svc.GetAllUsers();
            return Ok(results);
        }
    }
}