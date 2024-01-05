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

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        FullName = model.FullName,
                        Email = model.Email,
                        UserName = model.Username,
                        UserRole = model.UserRole,
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        // Assign the user to a role
                        var roleResult = await _userManager.AddToRoleAsync(user, model.UserRole);

                        if (roleResult.Succeeded)
                        {
                            // Automatically sign in the user after registration
                            await _signInManager.SignInAsync(user, isPersistent: false);

                            // Redirect to a secure page after successful registration
                            ViewData["user"] = new ApplicationUser
                            {
                                Id = user.Id,
                                UserName = user.UserName,
                                FullName = user.FullName,
                                Email = user.Email
                            };
                            
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    Debug.WriteLine(result.Errors);
                }
                // Registration failed! redisplay the registration page
                return View(model);
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
                Debug.WriteLine($"Failed to register user: {ex}");
                return View($"Error: {ex.Message}");
            }
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
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
                        var user = await _signInManager.UserManager.FindByNameAsync(model.Username);
                        if (user != null)
                        {
                            // Pass the necessary data to the view
                            ViewData["user"] = new ApplicationUser {
                                Id = user.Id, 
                                UserName=user.UserName,
                                FullName=user.FullName,
                                Email=user.Email
                            };

                            return RedirectToAction("Index", "Home");
                        }
                        ModelState.AddModelError("UserError", "Something happened! Try again.");
                    }
                    Debug.WriteLine(result);
                    ModelState.AddModelError("UsernamePasswordError", "Username or Password is incorrect");
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
