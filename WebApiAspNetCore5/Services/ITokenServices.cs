using System.Collections.Generic;
using System.Security.Claims;

namespace WebApiAspNetCore5.Services
{
    public interface ITokenServices
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();


        ClaimsPrincipal GetclaimsPrincipalFromExpiredToken(string token);

    }
}
