using Econference.Data;
using Econference.Models;
using Econference.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Econference.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IConferenceRepository _conferenceContext;
        public UserController(IConferenceRepository conferenceContext)
        {
            _conferenceContext = conferenceContext;   
        }

        // GET: UserController
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

        // GET: UserController/RegisterConference
        public ActionResult RegisterConference(string userId)
        {
            ViewData["UserId"] = userId;

            return View();
        }

        // POST: UserController/RegisterConference
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterConference(ConferenceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var conference = new Conference
                    {
                        UserId = model.UserId,
                        Title = model.Title,
                        Description = model.Description,
                        Equipment = model.Equipment,
                        ParticipantCount = model.ParticipantCount,
                        StartDate = DateOnly.ParseExact(model.StartDate, "yyyy-MM-dd"),
                        StartTime = TimeOnly.ParseExact(model.EndTime, "HH:mm"),
                        EndDate = DateOnly.ParseExact(model.EndDate, "yyyy-MM-dd"),
                        EndTime = TimeOnly.ParseExact(model.EndTime,"HH:mm"),
                        Status = model.Status,
                        CreatedAt = DateTime.Now,
                    };
                    await _conferenceContext.AddAsync(conference);
                    return RedirectToAction(nameof(ListConference));
                }
                return View(model);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                ModelState.AddModelError("Error:", e.Message);
                return View(model);
            }
        }

        // GET: UserController/ListConferences/
        public async Task<IActionResult> ListConference()
        {
            var userJson = HttpContext.Session.GetString("user");
            if (userJson == null)
            {
                return RedirectToAction("LogIn","Account");
            }
            var user = JsonConvert.DeserializeObject<ApplicationUser>(userJson);

            var conferences = await _conferenceContext.GetAllAsync();
           
            var userConferences = conferences.Where(u => u.UserId.Equals(user.Id));

            return View(userConferences);
        }


        // GET: UserController/DeleteConference/
        public async Task<IActionResult> DeleteConference(int id)
        {
            var userJson = HttpContext.Session.GetString("user");
            if (userJson == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            var user = JsonConvert.DeserializeObject<ApplicationUser>(userJson);

            try
            {
                await _conferenceContext.DeleteAsync(id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            var conferences = await _conferenceContext.GetAllAsync();

            var userConferences = conferences.Where(u => u.UserId.Equals(user.Id));

            return View(userConferences);
        }



        // GET: UserController/Details/5
        public async Task<IActionResult> UpdateConference(int id)
        {
            try
            {
                var conference = await _conferenceContext.GetAsync(id);
                return View(conference);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return RedirectToAction(nameof(ListConference));
        }

        // POST: UserController/UpdateConference
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConference(int id, ConferenceViewModel model)
        {
            try
            {
                var conference = await _conferenceContext.GetAsync(id);
                if (conference == null)
                {
                    return RedirectToAction(nameof(ListConference));
                }
                conference.Title = model.Title;
                conference.Description = model.Description;
                conference.Equipment = model.Equipment;
                conference.ParticipantCount = model.ParticipantCount;
                conference.StartDate = DateOnly.ParseExact(model.StartDate, "yyyy-MM-dd");
                conference.StartTime = TimeOnly.ParseExact(model.EndTime, "HH:mm");
                conference.EndDate = DateOnly.ParseExact(model.EndDate, "yyyy-MM-dd");
                conference.EndTime = TimeOnly.ParseExact(model.EndTime, "HH:mm");
                conference.Status = model.Status;

                await _conferenceContext.UpdateAsync(conference);
                return RedirectToAction(nameof(ListConference));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return RedirectToAction(nameof(ListConference));
            }
        }

        // GET: UserController/Create
        public async Task<IActionResult> BookConferenceSpace(int id)
        {
            try
            {
                var conference = await _conferenceContext.GetAsync(id);
                TempData["conference"] = conference;
                return View(conference);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return RedirectToAction(nameof(ListConference));
            }
        }

        // POST: UserController/BookConferenceSpace
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookConferenceSpace(ConferenceBookingViewModel model)
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
