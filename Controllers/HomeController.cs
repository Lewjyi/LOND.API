using System.Threading.Tasks;
using LOND.API.Auth;
using LOND.API.Interfaces;
using LOND.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace LOND.API.Controllers
{
    [ApiController]
    [ApiKeyAuthorize]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IUserService _signUpSignIn;
        public HomeController(IUserService signUpSignIn)
        {
            _signUpSignIn = signUpSignIn;
        }
        [HttpPost("ClassicSignUp")]
        public async Task<IActionResult> ClassicSignUp(SignUpObject signUpObject)
        {
            try
            {
 

                if (signUpObject == null)
                {
                    return BadRequest("SignUpObject cannot be null");
                }

                await _signUpSignIn.ClassicSignUpAsync(signUpObject);
                return Ok();


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("SSOSignUp")]
        public IActionResult SSOSignUp()
        {
            throw new NotImplementedException();
        }

        [HttpPost("ClassicSignIn")]
        public async Task<IActionResult> ClassicSignIn(SignInObject signInObject)
        {
            try
            {
                await _signUpSignIn.ClassicSignInAsync(signInObject);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("SSOSignIn")]
        public IActionResult SSOSignIn()
        {
            return Ok();
        }


    }
}
