using Microsoft.AspNetCore.Mvc;
using Task1.Models;

namespace Task1.Controllers
{
    public class CalcController : Controller
    {
        private readonly Calculator _calculator;
        public CalcController(Calculator calculator)
        {
            _calculator = calculator;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(double x, double y)
        {
            var result = _calculator.Add(x, y);

            return Content(result.ToString(), "text/plain");
        }

        public IActionResult Sub(double x, double y)
        {
            var result = _calculator.Sub(x, y);

            return Content(result.ToString(), "text/plain");
        }

        public IActionResult Mul(double x, double y)
        {
            var result = _calculator.Mul(x, y);

            return Content(result.ToString(), "text/plain");
        }

        public IActionResult Div(double x, double y)
        {
            var result = _calculator.Div(x, y);

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
