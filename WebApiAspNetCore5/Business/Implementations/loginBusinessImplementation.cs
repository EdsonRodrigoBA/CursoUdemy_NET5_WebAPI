using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiAspNetCore5.Configuration;
using WebApiAspNetCore5.Data.VO;
using WebApiAspNetCore5.Repository;
using WebApiAspNetCore5.Services;
using WebApiAspNetCore5.Services.Implementations;

namespace WebApiAspNetCore5.Business.Implementations
{
    public class loginBusinessImplementation : IloginBusiness
    {
        private const String DATE_FOMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _tokenConfiguration;
        private IusuarioRepository _iusuarioRepository;
        private readonly ITokenServices tokenServices;

        public loginBusinessImplementation(TokenConfiguration tokenConfiguration, IusuarioRepository iusuarioRepository, ITokenServices tokenServices)
        {
            _tokenConfiguration = tokenConfiguration;
            _iusuarioRepository = iusuarioRepository;
            this.tokenServices = tokenServices;
        }

        public TokenVO ValidateCredential(UsuariosVO usuarioCrendiciais)
        {

            var user = _iusuarioRepository.ValidarCredenciais(usuarioCrendiciais);
            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.user_name),
                new Claim(ClaimTypes.Role, user.Role),

            };

            var accesstoken = tokenServices.GenerateAccessToken(claims);
            var refreshtoken = tokenServices.GenerateRefreshToken();
            user.refresh_token = refreshtoken;
            user.refresh_token_expire_time = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry);
            DateTime createdDate = DateTime.Now;
            DateTime ExpirationDate = createdDate.AddMinutes(_tokenConfiguration.minutes);

            _iusuarioRepository.RefreshUsers(user);

            return new TokenVO(
                true,
                createdDate.ToString(DATE_FOMAT),
                ExpirationDate.ToString(DATE_FOMAT),
                 accesstoken,
                 refreshtoken
                );


        }

        public TokenVO ValidateCredential(TokenVO token)
        {
            var accesstoken = token.AccessToken;
            var refreshtoken = token.RefreshToken;

            var principal = tokenServices.GetclaimsPrincipalFromExpiredToken(accesstoken);
            var username = principal.Identity.Name;
            var user = _iusuarioRepository.ValidarCredenciais(username);
            if (user == null ||
                user.refresh_token != refreshtoken ||
                user.refresh_token_expire_time <= DateTime.Now)
            {
                return null;

            }
            accesstoken = tokenServices.GenerateAccessToken(principal.Claims);
            refreshtoken = tokenServices.GenerateRefreshToken();
            user.refresh_token = refreshtoken;
            user.refresh_token = refreshtoken;
            user.refresh_token_expire_time = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry);
            DateTime createdDate = DateTime.Now;
            DateTime ExpirationDate = createdDate.AddMinutes(_tokenConfiguration.minutes);

            _iusuarioRepository.RefreshUsers(user);

            return new TokenVO(
                true,
                createdDate.ToString(DATE_FOMAT),
                ExpirationDate.ToString(DATE_FOMAT),
                 accesstoken,
                 refreshtoken
                );

        }

        public bool RevokeToken(string user_name)
        {
            return _iusuarioRepository.RevokeToken(user_name);
        }
    }
}
