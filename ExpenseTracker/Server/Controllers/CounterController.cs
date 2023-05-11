using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CounterController
    {
        [HttpGet("sharingan")]
        public string Get()
        {
            return "sharingan";
        }
    }
}
