using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Task2.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            var browser = Request.Headers["User-Agent"].ToString();
            var Ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4();

            return $"Browser: {browser}, \nIP: {Ip}";
        }
    }
}
