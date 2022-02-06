using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models;
using WebApiAspNetCore5.Business;
using WebApiAspNetCore5.Data.VO;
using WebApiAspNetCore5.DB.HiperMidia.Filters;
using Microsoft.AspNetCore.Authorization;

namespace WebApiAspNetCore5.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize("Bearer")]
    public class PersonController : Controller
    {
        private IPersonBusiness _iPersonBusiness;
        public PersonController(IPersonBusiness iPersonBusiness)
        {
            this._iPersonBusiness = iPersonBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type =typeof(List<PersonVO>) )]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]

        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult Get()
        {
            return Ok(_iPersonBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMidiaFilter))]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var PersonVO = _iPersonBusiness.FindByID(id);
            if(PersonVO == null)
            {
                return NotFound();
            }
            return Ok(PersonVO);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMidiaFilter))]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] PersonVO PersonVO)
        {
            
            if (PersonVO == null)
            {
                return BadRequest();
            }
            return Ok(_iPersonBusiness.Create(PersonVO));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMidiaFilter))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] PersonVO PersonVO)
        {

            if (PersonVO == null)
            {
                return BadRequest();
            }
            return Ok(_iPersonBusiness.Update(PersonVO));
        }


        [HttpDelete("{id}")]
        [ProducesResponseType((204), Type = typeof(PersonVO))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
             _iPersonBusiness.Delete(id);
          
            return NoContent();
        }
    }
}
