using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdditionalTask.Model
{
    public class ProductsProvider
    {
        private List<Product> _products;

        public ProductsProvider()
        {
            _products = new List<Product> {
                new Product {Id=1, Name = "ProductA", Price = 15000 },
                new Product {Id=2, Name = "ProductB", Price = 10000 },
                new Product {Id=3, Name = "ProductC", Price = 12500 },
                new Product {Id=4, Name = "ProductD", Price = 18800 },
                new Product {Id=5, Name = "ProductE", Price = 14200 }
            };
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Remove(Product product)
        {
            _products.Remove(product);
        }

    }
}
