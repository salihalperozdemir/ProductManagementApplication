using Microsoft.IdentityModel.Tokens;
using ProductManagement.Business.Models.ResponseModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagement.Web.Api.Helpers
{
    public class AuthHelper
	{
        public string GenerateToken(LoginResponse model, IConfiguration configuration)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("username", model.Email));
            claims.Add(new Claim("displayname", model.FirstName));

            // Add roles as multiple claims
            foreach (var role in model.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWT:ValidIssuer"],
                Audience = configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
	}
	
}
