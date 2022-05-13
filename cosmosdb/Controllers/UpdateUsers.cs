using Microsoft.AspNetCore.Mvc;
using UsersInterfaceNSP;
using UsersModelNSP;

namespace UpdateUsersNSP
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateUsers : ControllerBase
    {
        private readonly IUsersMthodsInterface _svc;

        public UpdateUsers(IUsersMthodsInterface svc)
        {
            _svc = svc;
        }

        [HttpPut]
        public ActionResult updateUsers(UsersModel users, string id)
        {
            var results = _svc.UpdateUsers(id, users);
            return Ok(results);
        }
    }
}