using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task4.Controllers
{
    public class JsonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ClientInfo()
        {
            var client1 = new Client()
            {
                Id = 1,
                Login = "user1",
                Email = "user1@example.com"
            };

            var client2 = new Client()
            {
                Id = 2,
                Login = "user2",
                Email = "user2@example.com"
            };
            var client3 = new Client()
            {
                Id = 3,
                Login = "user3",
                Email = "user3@example.com"
            };
            var client4 = new Client()
            {
                Id = 4,
                Login = "user4",
                Email = "user4@example.com"
            };
            var client5 = new Client()
            {
                Id = 5,
                Login = "user5",
                Email = "user5@example.com"
            };

            Client[] clients = new Client[] { client1, client2, client3, client4, client5 };

            // Json - Сериализует объект переданный в параметрах в JSON и возвращает клиенту ответ.
            return Json(clients);
        }

        public IActionResult ClientInfo2()
        {
            // использование анонимных типов для формирования JSON ответа
            return Json(new
            {
                Id = 100,
                Login = "user1",
                Email = "user1@example.com"
            });
        }
    }
    public class Client
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}

