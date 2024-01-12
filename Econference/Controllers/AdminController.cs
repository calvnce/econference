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
        private readonly ICateringProviderRepository _cateringContext;
        public AdminController(IHallRepository hallContext, ICateringProviderRepository cateringContext) 
        {
            _hallContext = hallContext;
            _cateringContext = cateringContext;
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
        // Post: AdminController/AddConferenceHall
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: AdminController/ListConferenceHall
        public async Task<ActionResult> ListCateringProvider()
        {
            try
            {
                var caters = await _cateringContext.GetAllAsync();
                return View(caters);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return View();
            }
        }
        // GET: AdminController/AddCateringProvider
        public ActionResult AddCateringProvider()
        {
            return View();
        }

        // Post: AdminController/AddCateringProvider
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCateringProvider(CateringProviderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cater = new CateringProvider()
                    {
                        ProviderName = model.ProviderName,
                        Menu = model.Menu,
                        CreatedAt = DateTime.Now,
                    };
                    await _cateringContext.AddAsync(cater);
                    return RedirectToAction(nameof(ListCateringProvider));
                }
                return View(model);
            }
            catch (Exception e)
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
