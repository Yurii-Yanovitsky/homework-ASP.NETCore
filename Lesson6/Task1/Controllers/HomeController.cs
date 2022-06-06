using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Task1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReadOnlyCollection<string> _collection;

        public HomeController(IReadOnlyCollection<string> collection)
        {
            _collection = collection;
        }

        public string Index()
        {

            var result = string.Join(", ", _collection);

            return result;
        }
    }
}
