using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using WebApiAspNetCore5.Configuration;

namespace WebApiAspNetCore5.Services.Implementations
{
    public class TokenServices : ITokenServices
    {
        private readonly TokenConfiguration _tokenConfiguration;
        public TokenServices(TokenConfiguration tokenConfiguration)
        {
            this._tokenConfiguration = tokenConfiguration;
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._tokenConfiguration.secrete));
            var signcredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var options = new JwtSecurityToken(
                issuer: _tokenConfiguration.issuer,
                audience: _tokenConfiguration.audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_tokenConfiguration.minutes),
                signingCredentials: signcredential
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(options);
            return tokenString;

        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetclaimsPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._tokenConfiguration.secrete)),
                ValidateLifetime = false
            };

            var tokenHandle = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandle.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
            {
                throw new SecurityTokenException("Token  de acesso Inválido.");
            }

                return principal;
        }
    }
}
