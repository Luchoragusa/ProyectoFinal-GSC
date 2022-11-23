using Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite.Mathematics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplicationAPI.Configuration;

namespace WebApplicationAPI.Handlres
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _jwtOptions;

        public JwtHandler(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions?.Value ?? throw new ArgumentException(nameof(jwtOptions));
        }
        
        public string GenerateToken(Person user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }
        public SigningCredentials GetSigningCredentials()
        {

            var key = Encoding.UTF8.GetBytes(_jwtOptions.Key); //Esto debe ser configurable por ambiente. Secret Manager podria ser una solucion https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows 
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        
        public List<Claim> GetClaims(Person user)
        {
            string date = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd-HH-mm-ss");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Expiration, date)
            };


            return claims;
        }
        
        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
