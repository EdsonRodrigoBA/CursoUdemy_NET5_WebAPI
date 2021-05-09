using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAspNetCore5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculadoraController : ControllerBase
    {
     

        private readonly ILogger<CalculadoraController> _logger;

        public CalculadoraController(ILogger<CalculadoraController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(String firstNumber, String secondNumber)
        {
            if(isnumeric(firstNumber) & isnumeric(secondNumber))
            {
                var sum = Convert.ToDecimal(firstNumber) + Convert.ToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

                return BadRequest("valor invalido");
        }


        [HttpGet("Subtraction/{firstNumber}/{secondNumber}")]
        public IActionResult Subtraction(String firstNumber, String secondNumber)
        {
            if (isnumeric(firstNumber) & isnumeric(secondNumber))
            {
                var sum = Convert.ToDecimal(firstNumber) - Convert.ToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("valor invalido");
        }


        [HttpGet("Multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult Multiplication(String firstNumber, String secondNumber)
        {
            if (isnumeric(firstNumber) & isnumeric(secondNumber))
            {
                var sum = Convert.ToDecimal(firstNumber) * Convert.ToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("valor invalido");
        }


        [HttpGet("avg/{firstNumber}/{secondNumber}")]
        public IActionResult Avg(String firstNumber, String secondNumber)
        {
            if (isnumeric(firstNumber) & isnumeric(secondNumber))
            {
                var sum = (Convert.ToDecimal(firstNumber) + Convert.ToDecimal(secondNumber)) / 2;
                return Ok(sum.ToString());
            }

            return BadRequest("valor invalido");
        }
        private bool isnumeric(string strNumber)
        {
            double number;
            bool isNumber = Double.TryParse(strNumber, System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out number
                );
            return isNumber;
        }

        private decimal ConvertDecimal(string strNumber)
        {
            decimal decimalValue;
            if(Decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }
    }
}
