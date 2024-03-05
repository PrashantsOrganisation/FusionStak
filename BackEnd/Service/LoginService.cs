using FusionStackBackEnd.Models;
using FusionStackBackEnd.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace FusionStackBackEnd.Service
{
    public class LoginService
    {
        private LoginRepositoryImpl repo;
        private readonly IConfiguration _configuration;
        public LoginService(IConfiguration configuration, LoginRepositoryImpl repo)
        {
            this.repo = repo;
            _configuration = configuration;
        }
        public string isRightCredentials(string email, string password, string role)
        {
            var user = repo.getUser(email,password,role);
            if (user == null)
            {


                return null;

            }
            return GenerateJwtToken(user);

        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            Console.WriteLine(user.Role.Name);
   
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        // Add role claims
        new Claim(ClaimTypes.Role, user.Role.Name) 
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string EncryptPassword(string password)
        {

            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}