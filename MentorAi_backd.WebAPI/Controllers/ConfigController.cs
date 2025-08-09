using Microsoft.AspNetCore.Mvc;

namespace MentorAi_backd.WebAPI.Controllers
{
    [ApiController]
    [Route("api/config")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("frontend")]
        public IActionResult GetFrontendConfig()
        {
            var config = new
            {
                ApiBaseUrl = _configuration["FrontendConfig:ApiBaseUrl"]
            };

            return Ok(config);
        }
    }
}
