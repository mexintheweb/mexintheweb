using mexintheweb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mexintheweb.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private UserManager<IdentityUser> UserManager { get; }

        public LoginService(UserManager<IdentityUser> um)
        {
            UserManager = um;
        }

        public async Task<string> GetWebtokenByLogin(LoginModel login)
        {
            var result = "";
            var user = await UserManager.FindByNameAsync(login?.Username);

            if(user != null && await UserManager.CheckPasswordAsync(user, login?.Password))
            {
                var roles = await UserManager.GetRolesAsync(user);
                var claims = new List<Claim>();
                claims.Add(new Claim("username", user.UserName));
                claims.Add(new Claim("displayname", user.Email));

                if (roles != null && roles.Any())
                {
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                var token = GetJwt(user.UserName, claims.ToArray());
                var bearer = new JwtSecurityTokenHandler().WriteToken(token);

                if (!string.IsNullOrWhiteSpace(bearer))
                    result = bearer;
            }

            return result;
        }

        private JwtSecurityToken GetJwt(string userName, Claim[] claims = null)
        {
            var resultClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (claims is object)
            {
                var additionalClaims = new List<Claim>(resultClaims);
                additionalClaims.AddRange(claims);
                resultClaims = additionalClaims.ToArray();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SECURITYMEXWEB21SECURITYMEXWEB21SECURITYMEXWEB21SECURITYMEXWEB21SECURITYMEXWEB21SECURITYMEXWEB21"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var result = new JwtSecurityToken(
                issuer: "https://www.mexintheweb.net",
                audience: "https://www.mexintheweb.net",
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20.0)),
                claims: resultClaims,
                signingCredentials: creds);

            return result;
        }
    }
}
