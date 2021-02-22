using Microsoft.AspNetCore.Mvc;
using SimpleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleApp.Controllers
{
    public class ViewProductResult<TProduct>
    {
        public IEnumerable<TProduct> ProductsList { get; set; }
        public string CategoryName { get; set; }
        public ViewProductResult(IEnumerable<TProduct> productsList, string categoryName)
        {
            ProductsList = productsList;
            CategoryName = categoryName;
        }
    }


    public class ProductsController : Controller
    {
        private readonly ProductReader _reader;

        public ProductsController(ProductReader reader)
        {
            _reader = reader;
        }

        public IActionResult List(string category)
        {
            if (category == null)
            {
                var productsList = _reader.ReadFromFile();
                var resultSet = new ViewProductResult<Product>(productsList, null);

                return View(resultSet);
            }
            else if (_reader.ReadFromFile().Any(p => p.Category == category))
            {
                var categoryList = _reader.GetProductsByCategory(category);
                var resultSet = new ViewProductResult<Product>(categoryList, categoryList.Key);

                return View(resultSet);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Details(int id)
        {
            List<Product> products = _reader.ReadFromFile();

            if (products.Any(x => x.Id == id))
            {

                return View(products.First(p => p.Id == id));
            }
            else
            {

                return NotFound();
            }
        }
    }
}