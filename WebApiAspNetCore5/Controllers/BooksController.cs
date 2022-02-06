using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models;
using WebApiAspNetCore5.Business;
using Microsoft.AspNetCore.Authorization;

namespace WebApiAspNetCore5.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize("Bearer")]

    public class BooksController : Controller
    {
        private IBooksBusiness _ibookBusiness;
        public BooksController(IBooksBusiness ibookBusiness)
        {
            this._ibookBusiness = ibookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_ibookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var Books = _ibookBusiness.FindByID(id);
            if(Books == null)
            {
                return NotFound();
            }
            return Ok(Books);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BooksVO BooksVO)
        {
            
            if (BooksVO == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_ibookBusiness.Create(BooksVO));
        }

        [HttpPut]
        public IActionResult Put([FromBody] BooksVO BooksVO)
        {

            if (BooksVO == null)
            {
                return BadRequest();
            }
            return Ok(_ibookBusiness.Update(BooksVO));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
             _ibookBusiness.Delete(id);
          
            return NoContent();
        }
    }
}
