using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models;
using WebApiAspNetCore5.Business;

namespace WebApiAspNetCore5.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : Controller
    {
        private IPersonBusiness _ipersonBusiness;
        public PersonController(IPersonBusiness ipersonBusiness)
        {
            this._ipersonBusiness = ipersonBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_ipersonBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _ipersonBusiness.FindByID(id);
            if(person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            
            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_ipersonBusiness.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {

            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_ipersonBusiness.Update(person));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
             _ipersonBusiness.Delete(id);
          
            return NoContent();
        }
    }
}
