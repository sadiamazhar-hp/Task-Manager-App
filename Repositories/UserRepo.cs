using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManager.Interface;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class UserRepo : IUser
    {
        private readonly IConfiguration _configuration;
       
        private List<User> Users = new()
        {
            new User {ID = 0,UserName="Saif",Email="saif2@gmail.com",Password="3829", Role=Roles.TeamLead},
            new User {ID = 1,UserName="Agha Khan",Email="Agha112@gmail.com",Password="1090", Role= Roles.TeamMemeber}
        };
        public UserRepo(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public User RegisterUser(User user)
        {
            var usertoadd = new User
            {
                ID = Users.Count + 1, // Assign a unique ID (this may differ based on your actual database)
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Password = user.Password
            };

            Users.Add(usertoadd);

            // Return a simplified response without token after registration
            return new User { UserName = usertoadd.UserName, Role = usertoadd.Role };

            return  usertoadd ;

             
        }

        public UserDto Authenticate(User user)
        {
            var userToAuth = Users.SingleOrDefault(x => x.UserName == user.UserName &&
                                                         x.Email == user.Email &&
                                                         x.Password ==user.Password );

            if (userToAuth == null)
            {
                return null; // User not found or credentials are incorrect
            }

            // Generate JWT token for the authenticated user
            var token = GenerateJwtToken(userToAuth);

            // Return user details including token
            return new UserDto
            {
                UserName = userToAuth.UserName,
                Email = userToAuth.Email,
                Token = token,
                Role = userToAuth.Role
            };
        }

        public User? GetEmailandPassword(string email, string password)
        {
            var user = Users.First(x => x.Email == email && x.Password ==password);
            return user;

        }
        public IEnumerable<User> AllUsers()
        {
            return Users;
        }

        public User UnregisterUser(int id)
        {
            throw new NotImplementedException();
        }


        //Methods

        //to Generate token
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()), new Claim(ClaimTypes.Role, user.Role.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiresInMinute"])),
                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        


    }
}
