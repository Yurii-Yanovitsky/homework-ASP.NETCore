using Microsoft.AspNetCore.Mvc;
using SimpleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductReader _reader;

        public ProductsController()
        {
            _reader = new ProductReader();
        }

        public IActionResult List()
        {
            List<Product> products = _reader.ReadFromFile();

            return View(products);
        }

        public IActionResult Details(int id)
        {
            List<Product> products = _reader.ReadFromFile();
            Product product = products.Where(x => x.Id == id).FirstOrDefault();

            if (product != null)
            {

                return View(product);
            }
            else
            {

                return NotFound();
            }
        }
    }
}