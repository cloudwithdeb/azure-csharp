using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;


namespace durable_functions
{
    public static class SignupStarterFunction
    {
        [FunctionName("SignupOrchestrationFunction")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();
            EmployeesRequest data = context.GetInput<EmployeesRequest>();

            foreach(var emp in data.Employee){
                outputs.Add(await context.CallActivityAsync<string>("SignupStarterFunction_Hello", emp.email));
            }

            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallActivityAsync<string>("SignupStarterFunction_Hello", "Tokyo"));
           
            return outputs;
        }

        [FunctionName("SignupActivityFunctionCheckUsersDetails")]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }

        [FunctionName("SignupStarterFunction")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Get input dataset from client
            var data = req.Content.ReadAsAsync<EmployeesRequest>();

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("SignupOrchestrationFunction", data);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}