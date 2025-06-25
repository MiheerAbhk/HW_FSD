using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Practice.Controllers
{

    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [Route("Emp/All")]
        [HttpGet]
        public string Get()
        {
            return "Response from Employee Controller-Get Method";
        }
        [Route("Emp/ById")]
        [HttpGet]
        public string GetEmployee()
        {
            return "Response from Employee Controller-GetEmployee Method";
        }
    }
}
