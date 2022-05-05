using Microsoft.AspNetCore.Mvc;
using UsersInterfaceNSP;
using UsersModelNSP;

namespace CreateContainerNSP
{
    [ApiController]
    [Route("[controller]")]
    public class CreateDatabaseAndContainer : ControllerBase
    {
        // Define variable for interface
        private readonly IUsersMthodsInterface _svc;

        // Define constructor
        public CreateDatabaseAndContainer(IUsersMthodsInterface svc)
        {
            _svc = svc;
        }

        // Define constructor
        [HttpPost]
        public ActionResult CreatContainer(DataBaseCol dbcont)
        {
            var results = _svc.CreateDatabaseAndCollection(dbcont);
            return Ok(results);
        }
    }
}