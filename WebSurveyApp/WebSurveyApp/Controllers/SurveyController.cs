using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSurveyApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly SurveyDbContext _context;

        public SurveyController(SurveyDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] Survey surveyModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

                user.Surveys.Add(surveyModel);

                await _context.SaveChangesAsync();

                return RedirectToAction($"Create", "Question", new { surveyId = surveyModel.Id });
            }

            return View(surveyModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(Survey surveyModel)
        {
            if (ModelState.IsValid)
            {
                var survey = await _context.Surveys.FindAsync(surveyModel.Id);
                survey.Title = surveyModel.Title;
                await _context.SaveChangesAsync();

                return RedirectToAction($"Edit", "Survey", new { survey.Id });
            }

            return NotFound();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit([FromRoute] int surveyId)
        {
            var survey = await _context.Surveys
                .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(s => s.Id == surveyId);

            if (survey != null)
            {

                return View(survey);
            }

            return View("Create");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int surveyId)
        {
            var survey = await _context.Surveys
                .Include("Reports")
                .Include("Reports.Responses")
                .AsSplitQuery()
                .FirstOrDefaultAsync(r => r.Id == surveyId);

            if (survey != null)
            {
                var responses = survey.Reports.SelectMany(x => x.Responses);

                if (responses != null)
                {
                    _context.Responses.RemoveRange(responses);
                }

                _context.Surveys.Remove(survey);
                await _context.SaveChangesAsync();

                return RedirectToAction("List", "Home");
            }

            return NotFound();
        }
    }
}
