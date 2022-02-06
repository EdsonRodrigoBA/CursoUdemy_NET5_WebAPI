using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Data.VO;

namespace WebApiAspNetCore5.Business
{
    public interface IloginBusiness
    {
        TokenVO ValidateCredential(UsuariosVO usuarios);

        TokenVO ValidateCredential(TokenVO token);

        bool RevokeToken(string user_name);


    }
}
