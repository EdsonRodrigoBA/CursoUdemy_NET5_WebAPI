using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Business;
using WebApiAspNetCore5.Data.VO;

namespace WebApiAspNetCore5.Controllers
{

    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IloginBusiness _iloginBusiness;

        public AuthController(IloginBusiness iloginBusiness)
        {
            _iloginBusiness = iloginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UsuariosVO user)
        {
            if (user == null)
            {
                return BadRequest("Usuario invalido");
            }

            var token = _iloginBusiness.ValidateCredential(user);
            if (token == null)
            {
                return Unauthorized("Acesso não peritido");
            }
            return Ok(token);
        }

        [HttpPost]
        [Route("refreshtoken")]
        public IActionResult RefreshToken([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null)
            {
                return BadRequest("Solicitação inválida.");
            }

            var token = _iloginBusiness.ValidateCredential(tokenVO);
            if (token == null)
            {
                return BadRequest("Solicitação inválida.");
            }
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]

        public IActionResult revoke()
        {

            var username = User.Identity.Name;
            var result = _iloginBusiness.RevokeToken(username);
            if (!result)
            {
                return BadRequest("Solicitação inválida.");

            }
            return NoContent();
        }
    }
}
