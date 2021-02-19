using AdditionalTask.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdditionalTask.Controllers
{
    public class CalcController : Controller
    {
        private readonly Calculator _calculator = new Calculator();
        private readonly string _view = "OperationResult";
        public CalcController(Calculator calculator)
        {
            _calculator = calculator;
        }

        public IActionResult Add(double x, double y)
        {
            return View(_view, _calculator.Add(x, y));
        }

        public IActionResult Sub(double x, double y)
        {
            return View(_view, _calculator.Sub(x, y));
        }

        public IActionResult Mul(double x, double y)
        {
            return View(_view, _calculator.Mul(x, y));
        }

        public IActionResult Div(double x, double y)
        {
            var result = _calculator.Div(x, y);

            if (result.IsValid)
            {

                return View(_view, result.Result);
            }
            else
            {
                return View(_view, result.Error);
            }
        }
    }
}
