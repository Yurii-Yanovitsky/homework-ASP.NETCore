using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSurveyApp.Controllers
{
    public class OptionController : Controller
    {
        private readonly SurveyDbContext _context;

        public OptionController(SurveyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create([FromRoute] int surveyId, [FromQuery] int questionId)
        {

            var optionModel = new Option() { QuestionId = questionId };

            ViewBag.SurveyId = surveyId;

            return View(optionModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] int surveyId, [FromForm] Option optionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Options.Add(optionModel);

                await _context.SaveChangesAsync();

                return RedirectToAction($"Edit", "Survey", new { surveyId });
            }

            return View(optionModel);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit([FromRoute] int surveyId, [FromQuery] int optionId)
        {

            var optionModel = await _context.Options.FindAsync(optionId);

            if (optionModel != null)
            {
                ViewBag.SurveyId = surveyId;

                return View(optionModel);
            }

            return NotFound();

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit([FromForm] int surveyId, [FromForm] Option optionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Options.Update(optionModel);

                await _context.SaveChangesAsync();

                return RedirectToAction($"Edit", "Survey", new { surveyId });
            }

            return View(optionModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int surveyId, [FromQuery] int optionId)
        {
            var option = await _context.Options.FindAsync(optionId);

            if (option != null)
            {
                _context.Options.Remove(option);

                await _context.SaveChangesAsync();

                return RedirectToAction($"Edit", "Survey", new { surveyId });
            }

            return NotFound();
        }
    }
}
