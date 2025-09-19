using Microsoft.AspNetCore.Mvc;

namespace LOND.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet("ClassicSignUp")]
        public IActionResult ClassicSignUp()
        {
            return Ok();
        }

        [HttpGet("SSOSignUp")]
        public IActionResult SSOSignUp()
        {
            return Ok();
        }

        [HttpGet("ClassicSignIn")]
        public IActionResult ClassicSignIn()
        {
            return Ok();
        }

        [HttpGet("SSOSignIn")]
        public IActionResult SSOSignIn()
        {
            return Ok();
        }


    }
}
