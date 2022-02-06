using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApiAspNetCore5.Data.VO;
using WebApiAspNetCore5.Models;
using WebApiAspNetCore5.Models.Context;

namespace WebApiAspNetCore5.Repository
{
    public class UsuarioRepository : IusuarioRepository
    {
        private readonly MySqlContext _mySqlContext;

        public UsuarioRepository(MySqlContext mySqlContext)
        {
            this._mySqlContext = mySqlContext;
        }
        public Usuarios ValidarCredenciais(UsuariosVO model)
        {

            var pass = ComputHash(model.password, new SHA256CryptoServiceProvider());

            return _mySqlContext.Usuarios.FirstOrDefault(x => (x.user_name == model.user_name) && (x.password == pass));
        }

        public Usuarios RefreshUsers(Usuarios user)
        {
            try
            {
                if (!_mySqlContext.Usuarios.Any(x => x.id == user.id))
                {
                    return null;
                }

                var result = _mySqlContext.Usuarios.SingleOrDefault(p => p.id.Equals(user.id));
                if (result != null)
                {
                    _mySqlContext.Entry(result).CurrentValues.SetValues(user);
                    _mySqlContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Usuarios ValidarCredenciais(string user_name)
        {
            return _mySqlContext.Usuarios.FirstOrDefault(u => u.user_name == user_name);
        }

        private string ComputHash(string imput, SHA256CryptoServiceProvider algotitimo)
        {
            Byte[] imputbytes = Encoding.UTF8.GetBytes(imput);
            Byte[] hashbytes = algotitimo.ComputeHash(imputbytes);

            return BitConverter.ToString(hashbytes);
        }

        public bool RevokeToken(string user_name)
        {
            var user = _mySqlContext.Usuarios.FirstOrDefault(u => u.user_name == user_name);
            if (user == null)
            {
                return false;
            }
            user.refresh_token = null;
            _mySqlContext.SaveChanges();
            return true;
        }
    }
}
