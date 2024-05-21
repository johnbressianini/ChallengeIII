using ChallengeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeApi.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "john" && password == "123456")
            {
                var token = TokenService.GenerateToken(new Models.User());
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }
    }
}
