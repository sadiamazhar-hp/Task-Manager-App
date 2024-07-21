using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Interface;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUser _user;

        public AuthController(IUser user)
        {
            _user = user;
        }

        [HttpGet("GetUsers")]
        
        public IActionResult GetUsers()
        {
            var allUsers = _user.AllUsers().ToList();
            return Ok(allUsers); // Wrap the list of users in an Ok() result
        }

        [HttpPost("SignUp")]
        public IActionResult Signup([FromBody] User user) {
            if (user == null)
            {
                _user.RegisterUser(user);
                return Ok("User Has been Added");
            }
            return BadRequest("User Has not been SignedUp");
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User userdata)
        {
            var response = _user.Authenticate(userdata);
            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
            //return RedirectToAction("AllTasks","TaskAPI");
        }
       

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("your_secret_key");

            var claims = new List<Claim>
        {
          new Claim(ClaimTypes.Name, user.UserName),
          new Claim(ClaimTypes.Email, user.Email),
          new Claim(ClaimTypes.Role,user.Role.ToString())
        };

            /*Add roles as claims if multiple ROles
            var roles = user.Role.Split();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }*/

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
