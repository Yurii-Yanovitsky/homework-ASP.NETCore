using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebSurveyApp.Controllers
{
    public class TakeSurveyController : Controller
    {
        private readonly SurveyDbContext _context;

        public TakeSurveyController(SurveyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Start(int surveyId)
        {
            var surveyModel = await _context.Surveys
                .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(s => s.Id == surveyId);

            if (surveyModel != null)
            {
                return View(surveyModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Finish(Report reportModel)
        {

            if (reportModel != null)
            {

                await _context.Reports.AddRangeAsync(reportModel);
                await _context.SaveChangesAsync();

                return View();
            }

            return NotFound();
        }
    }
}