using DemoCustomerSvc.Model;
using DemoCustomerSvc.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoCustomerSvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest();
            }

            if (loginModel.username == _configuration.GetValue<string>("UserAuth:Username") && loginModel.password == _configuration.GetValue<string>("UserAuth:Password"))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AuthKeys:Secretkey")));
                var signingCredentials = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration.GetValue<string>("AuthKeys:Issuer"),
                    audience: _configuration.GetValue<string>("AuthKeys:Audience"),
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signingCredentials
                    ); 

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


                return Ok(new AuthenticatedResponse() { Token = tokenString });
            }

            return Unauthorized();
        }
    }
}
