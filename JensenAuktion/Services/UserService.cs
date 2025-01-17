using Azure;
using JensenAuktion.Repository.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JensenAuktion.Services
{
    public class UserService
    {


        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>

            {
                new Claim(ClaimTypes.Name, user.UserName),
                //new Claim("UserID", user.UserID.ToString()), 
                new Claim("UserID", user.UserID.ToString()), 
                //new Claim(ClaimTypes.Role, "Admin")
            };
            //claims.Add(new Claim(ClaimTypes.Role, "Admin"));


            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mykey1234567&%%485734579453%&//1255362"));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            
            var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5247",
                    audience: "http://localhost:5247",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            //return tokenString;
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}

