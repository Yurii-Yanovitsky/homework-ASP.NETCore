using AdditionalTask.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdditionalTask.Controllers
{
    public class ProductsController : Controller
    {
        public ProductsProvider _provider { get; set; }
        public ProductsController(ProductsProvider provider)
        {
            _provider = provider;
        }
        public IActionResult Index()
        {
            ViewBag.Products = _provider.GetAllProducts();

            return View();
        }

        public IActionResult Test()
        {
            IEnumerable<Product> products = _provider.GetAllProducts();

            ViewProductsResult result = new ViewProductsResult()
            {
                Products = products,
                Count = products.Count()
            };


            return View(result);
        }
    }

    public class ViewProductsResult
    {
        public IEnumerable<Product> Products { get; set; }
        public int Count { get; set; }
    }
}
