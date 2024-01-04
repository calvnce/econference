using Econference.Data;
using Econference.Models;
using Econference.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Econference.Controllers
{
    public class AccountController(IUserRepository repository, PasswordHasher<ApplicationUser> passwordHasher) : Controller
    {
        private readonly PasswordHasher<ApplicationUser> _passwordHasher = passwordHasher;
        private readonly IUserRepository _db = repository;

        [HttpPost("account/register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Hash the password
                    // TODO: Save data to the database
                    // TODO: Redirect user to the login page ("/user/account/login")

                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
                return View("Error");
            }
        }

        [HttpPost("account/login")]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogInViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Validate the password with the help of PasswordHasher
                    // TODO: Redirect the user to the appropriate dashboard based on the role

                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
                return View("Error");
            }
        }
    }

}
