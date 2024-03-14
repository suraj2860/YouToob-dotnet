using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YouToob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public string HealthCheck()
        {
            return "Healthcheck passed";
        }
    }
}
