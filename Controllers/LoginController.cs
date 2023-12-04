using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RESTWebApp.Example.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace RESTWebApp.Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous] 
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(UserModel userModel)
        {
            var secruityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(secruityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(1200),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Task<UserModel?> AuthenticateUser(UserModel login)
        {
            return Task.Run(() =>
            {
                UserModel? user = null;

                if (login.UserName.Equals("Shiawase"))
                {
                    user = new UserModel { UserName = "Shiawase", Email = "Shiawase@gmail.com" };
                }

                return user;
            });
        }
    }
}
