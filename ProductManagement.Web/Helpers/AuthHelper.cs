using ProductManagement.Business.Models.ResponseModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProductManagement.Web.Helpers
{
    public class AuthHelper
    {
        public TokenResponse CreateToken(LoginResponse model, IConfiguration _configuration)
        {
            var tokenResponse = new TokenResponse();
            var claims = new List<Claim>();
            claims.Add(new Claim("username", model.Email));
            claims.Add(new Claim("displayname", model.FirstName));

            // Add roles as multiple claims
            foreach (var role in model.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            // create a new token with token helper and add our claim
            var token = JwtHelper.GetJwtToken(
                model.Email,
                _configuration["JWT:Secret"],
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                TimeSpan.FromMinutes(3600),
                claims.ToArray());

            tokenResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
            tokenResponse.Claims = claims;

            return tokenResponse;

        }
    }
    public class TokenResponse
    {
        public string Token { get; set; }
        public List<Claim> Claims { get; set; }
    }
}

