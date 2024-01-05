using Econference.Data;
using Econference.Models;
using Econference.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Econference.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        [HttpGet("account/register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost("account/register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
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
                Debug.WriteLine($"Failed to register user: {ex}");
                return View($"Error: {ex.Message}");
            }
        }

        [HttpGet("account/login")]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost("account/login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(LogInViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var user =  await _signInManager.UserManager.FindByNameAsync(model.Username);
                        if (user != null)
                        {
                            return RedirectToAction("Index", "Home", user);
                        }
                        ModelState.AddModelError("UserError", "Something happened! Try again.");
                    }
                    ModelState.AddModelError("UsernamePassowrdError", "Username or Password is incorrect");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
                Debug.WriteLine($"Failed to log in: {ex}");
                return View($"Error: {ex.Message}");
            }
        }
    }

}
