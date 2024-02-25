using API.Business.Model;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "admin" && password == "123")
            {
                var token = AuthService.GetToken(new Emprestimo());
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }
    }
}
