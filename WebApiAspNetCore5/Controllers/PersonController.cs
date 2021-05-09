using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models;
using WebApiAspNetCore5.Services;

namespace WebApiAspNetCore5.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : Controller
    {
        private IPersonService _ipersonService;
        public PersonController(IPersonService ipersonService)
        {
            this._ipersonService = ipersonService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_ipersonService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _ipersonService.FindByID(id);
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
            return Ok(_ipersonService.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {

            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_ipersonService.Update(person));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
             _ipersonService.Delete(id);
          
            return NoContent();
        }
    }
}
