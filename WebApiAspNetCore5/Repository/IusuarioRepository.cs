using WebApiAspNetCore5.Data.VO;
using WebApiAspNetCore5.Models;

namespace WebApiAspNetCore5.Repository
{
    public interface IusuarioRepository
    {
        Usuarios ValidarCredenciais(UsuariosVO model);
        Usuarios ValidarCredenciais(string user_name);

        Usuarios RefreshUsers(Usuarios user);

        bool RevokeToken(string user_name);
    }
}
