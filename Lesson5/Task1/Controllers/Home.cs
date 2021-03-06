using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Task1.Controllers
{
    public class Home : Controller
    {
        public const string _userDataKey = "userdata";
        public static Dictionary<string, string> Sessions { get; } = new Dictionary<string, string>();
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(string value)
        {
            var currentSession = HttpContext.Session;
            var key = HttpContext.Request.Headers["User-Agent"];

            if (HttpContext.Request.Cookies["SessionKey"] == null)
            {
                currentSession.SetString(_userDataKey, value);
                Sessions.Add(key, currentSession.Id);
            }

            else if (Sessions.TryGetValue(key, out string sessionId))
            {
                if (currentSession.Id != sessionId)
                {
                    currentSession.SetString(_userDataKey, value);
                    Sessions[key] = sessionId;
                }
            }

            return RedirectToAction("Test");
        }

        public IActionResult Test()
        {
            var currentSession = HttpContext.Session;

            if (currentSession.Keys.Any())
            {
                var model = HttpContext.Session.GetString(_userDataKey);
                ViewBag.Count = Sessions.Count();

                return View(model as object);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
