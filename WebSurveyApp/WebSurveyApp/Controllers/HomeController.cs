using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebSurveyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SurveyDbContext _context;

        public HomeController(SurveyDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var surveys = await _context.Surveys
                .Include(s => s.Reports)
                .Where(s => s.User.Email == User.Identity.Name)
                .ToListAsync();

            return View(surveys);
        }
    }
}
