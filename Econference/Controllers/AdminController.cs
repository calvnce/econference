using Econference.Data;
using Econference.Models;
using Econference.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Econference.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHallRepository _hallContext;
        public AdminController(IHallRepository hallContext) 
        {
            _hallContext = hallContext;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            var userJson = HttpContext.Session.GetString("user");
            // If user is not logged in, redirect to login page
            if (userJson == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            return View();
        }
        // GET: AdminController/ListConferenceHall
        public async Task<ActionResult> ListConferenceHall()
        {
            try
            {
                var halls = await _hallContext.GetAllAsync();
                return View(halls);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return View();
            }
        }

        // GET: AdminController/AddConferenceHall
        public ActionResult AddConferenceHall()
        {
            return View();
        }

        // GET: AdminController/Create
        public async Task<ActionResult> AddConferenceHall(HallViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hall = new Hall()
                    {
                        RoomNumber = model.RoomNumber,
                        BuildingFloor = model.BuildingFloor,
                        BuildingName = model.BuildingName,
                        Capacity = model.Capacity,
                        Status = "Available",
                        CreatedAt = DateTime.Now,
                    };
                    await _hallContext.AddAsync(hall);
                    return RedirectToAction(nameof(ListConferenceHall));
                }
                return View(model);
            }
            catch (Exception  e)
            {
                ModelState.AddModelError(String.Empty, e.Message);
                Debug.WriteLine(e.Message);
                return View(model);
            }
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
