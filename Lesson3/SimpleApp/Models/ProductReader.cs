using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Models
{
    public class ProductReader
    {
        private readonly string path = "App_Data/data.txt";

        public List<Product> ReadFromFile()
        {
            string[] lines = File.ReadAllLines(path);

            List<Product> result = new List<Product>();
            foreach (string line in lines)
            {
                string[] items = line.Split(',');

                Product product = new Product();
                product.Id = Convert.ToInt32(items[0].Trim());
                product.Name = items[1].Trim();
                product.Price = Convert.ToDouble(items[2].Trim());
                product.Description = items[3].Trim();
                product.Quantity = Convert.ToInt32(items[4].Trim());
                product.Category = items[5].Trim();

                result.Add(product);
            }

            return result;
        }

        public IGrouping<string, Product> GetProductsByCategory(string category)
        {
            var categoryResult = ReadFromFile()
                .Where(p => p.Category == category)
                .GroupBy(p => p.Category)
                .FirstOrDefault();

            return categoryResult;
        }
    }
}
