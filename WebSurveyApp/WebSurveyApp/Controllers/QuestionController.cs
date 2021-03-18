using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebSurveyApp.Controllers
{
    public class QuestionController : Controller
    {
        private readonly SurveyDbContext _context;

        public QuestionController(SurveyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create([FromRoute] int surveyId)
        {
            var questionModel = new Question() { SurveyId = surveyId };

            return View(questionModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] Question questionModel)
        {
            if (ModelState.IsValid)
            {
                await _context.Questions.AddAsync(questionModel);
                await _context.SaveChangesAsync();

                int questionId = questionModel.Id;

                return RedirectToAction($"Create", "Option", new { questionModel.SurveyId, questionId });
            }

            return View(questionModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit([FromQuery] int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question != null)
            {

                return View(question);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit([FromForm] Question questionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Questions.Update(questionModel);
                await _context.SaveChangesAsync();

                return RedirectToAction("Edit", "Survey", new { questionModel.SurveyId });
            }

            return View(questionModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete([FromForm] Question questionModel)
        {

            if (questionModel != null)
            {
                var response = await _context.Responses.FirstOrDefaultAsync(r => r.QuestionId == questionModel.Id);

                if (response != null)
                {
                    _context.Remove(response);
                }

                _context.Questions.Remove(questionModel);

                await _context.SaveChangesAsync();

                return RedirectToAction($"Edit", "Survey", new { questionModel.SurveyId });
            }

            return NotFound();
        }
    }
}
