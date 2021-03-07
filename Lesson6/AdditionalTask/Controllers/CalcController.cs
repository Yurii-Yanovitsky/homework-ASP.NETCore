using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AdditionalTask.Controllers
{
    public class CalcController : Controller
    {
        private readonly CalculatorService _calculator;

        public CalcController(CalculatorService calculator)
        {
            _calculator = calculator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(double a, double b)
        {

            var result = _calculator.Add(a, b);

            return Content(result.ToString());
        }

        [HttpPost]
        public IActionResult Sub(double a, double b)
        {
            var result = _calculator.Sub(a, b);

            return Content(result.ToString());
        }

        [HttpPost]
        public IActionResult Mul(double a, double b)
        {
            var result = _calculator.Mul(a, b);

            return Content(result.ToString());
        }

        [HttpPost]
        public IActionResult Div(double a, double b)
        {
            var result = _calculator.Div(a, b);

            if (result.IsValid)
            {
                return Content(result.Result.ToString());
            }
            else
            {
                return Content(result.Error);
            }
        }
    }
}
