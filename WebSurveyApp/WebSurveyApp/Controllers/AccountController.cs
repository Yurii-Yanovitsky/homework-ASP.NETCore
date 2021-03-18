using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebSurveyApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SurveyDbContext _context;

        public AccountController(SurveyDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginBindingModel model)
        {
            if (ModelState.IsValid)
            {

                var userOrNull = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (userOrNull is { } user)
                {
                    var isCorrectPassword = PasswordHasher.IsCorrectPassword(user, model.Password);
                    if (isCorrectPassword)
                    {
                        await SignInAsync(user);

                        return RedirectToAction("List", "Home");
                    }

                    return Unauthorized();
                }

                ModelState.AddModelError("", "The provided data are incorrect");
            }

            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Success");
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {

                var userSlreadyExists = _context.Users.Any(u => u.Email == model.Email);

                if (userSlreadyExists)
                {
                    ModelState.AddModelError(nameof(model.Email), "Login is already in use");
                    return View(model);
                }

                var user = new User()
                {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = PasswordHasher.HashPassword(model.Password),
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Success");
            }

            return View(model);
        }

        public IActionResult Success()
        {
            ViewBag.Username = User.Identity.Name;

            return View();
        }

        private async Task SignInAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name, ClaimTypes.Role);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);
        }
    }
}
