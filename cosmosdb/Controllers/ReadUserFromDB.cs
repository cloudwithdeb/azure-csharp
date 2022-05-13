using Microsoft.AspNetCore.Mvc;
using UsersInterfaceNSP;
using UsersModelNSP;

namespace ReadUserFromDBNSP
{
    [ApiController]
    [Route("controller")]
    public class ReadUserFromDB : ControllerBase
    {
        private readonly IUsersMthodsInterface _svc;

        public ReadUserFromDB(IUsersMthodsInterface svc)
        {
            _svc = svc;
        }

        [HttpGet("{id}")]
        public ActionResult GetCosmosDBUserById(string id)
        {
            List<UsersModel> results = _svc.GetUserFromCosmosDBUsingId(id);
            Console.WriteLine(results);
            if(results.Count > 0){
                return Ok(results[0]);
            }else{
                return Ok("User does not exists");
            }
        }
    }
}