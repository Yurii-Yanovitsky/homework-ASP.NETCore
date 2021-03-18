using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebSurveyApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly SurveyDbContext _context;

        public ReportController(SurveyDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> List(int surveyId)
        {

            var reports = await _context.Reports
                .Include("Responses")
                .Include("Responses.Question")
                .Include("Responses.Option")
                .AsSplitQuery()
                .Where(qs => qs.SurveyId == surveyId)
                .ToListAsync();

            if (reports != null)
            {

                return View(reports);
            }

            return NotFound();
        }
    }
}