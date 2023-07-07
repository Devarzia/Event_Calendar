using EventCalendar.Application;
using EventCalendar.Domain;
using EventCalendar.Filters;
using EventCalendar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EventCalendar.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ISocialEventService _socialEventService;
        private readonly IEventCategoryService _eventCategoryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventController(ISocialEventService socialEventService, IEventCategoryService eventCategoryService, UserManager<ApplicationUser> userManager)
        {
            _socialEventService = socialEventService;
            _eventCategoryService = eventCategoryService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index() => View();


        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _socialEventService.GetAllSocialEvents();
            var list = new List<SocialEventViewModel>();

            foreach (var e in events)
            {
                list.Add(new SocialEventViewModel
                {
                    Title = e.SocialEventName,
                    SocialEventID = e.SocialEventID,
                    Description = e.SocialEventDescription,
                    AllDay = e.AllDay,
                    Start = e.StartTime.ToString("s"),
                    End = e.EndTime?.ToString("s")
                });
            }

            return Json(list);
        }

        public async Task<IActionResult> UserEvents()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var events = await _socialEventService.GetSocialEventsForUser(user.Id);

            var model = new EventViewModel
            {
                Events = events
            };

            return View(model);
        }

        public async Task<JsonResult> GetSocialEventByID(int socialEventID)
        {
            var socialEvent = await _socialEventService.GetSocialEventByID(socialEventID);
            return new JsonResult(socialEvent);
        }

        public async Task<IActionResult> Details(int ID)
        {
            var socialEvent = await _socialEventService.GetSocialEventByID(ID);

            var model = new EventViewModel
            {
                SocialEvent = socialEvent
            };

            return View(model);
        }

        public async Task<JsonResult> AddSocialEvent()
        {
            var categories = await _eventCategoryService.GetAllCategories();

            return new JsonResult(categories);
        }

        [HttpPost, ValidateAntiForgeryToken, ModelValidationFilter]
        public async Task<IActionResult> AddSocialEvent(EventViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.SocialEvent.UserID = user.Id;
            await _socialEventService.AddSocialEvent(model.SocialEvent);
            TempData["Message"] = $"Success! \"{model.SocialEvent.SocialEventName}\" has been added!";
            Trace.TraceInformation($"{user.FirstName} {user.LastName} has created {model.SocialEvent.SocialEventName}");
            return RedirectToAction("UserEvents", "Event");
        }

        public async Task<JsonResult> EditSocialEvent(int socialEventID)
        {
            var socialEvent = await _socialEventService.GetSocialEventByID(socialEventID);
            var categories = await _eventCategoryService.GetAllCategories();

            if (socialEvent != null)
            {
                var model = new EventViewModel
                {
                    SocialEvent = socialEvent,
                    Categories = categories
                };

                return new JsonResult(model);
            }

            return null;
        }

        [HttpPost, ValidateAntiForgeryToken, ModelValidationFilter]
        public async Task<IActionResult> EditSocialEvent(EventViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.SocialEvent.UserID = user.Id;
            await _socialEventService.EditSocialEvent(model.SocialEvent);
            TempData["Message"] = $"Success! \"{model.SocialEvent.SocialEventName}\" has been edited!";
            Trace.TraceInformation($"{user.FirstName} {user.LastName} has edited {model.SocialEvent.SocialEventName}");
            return RedirectToAction("UserEvents", "Event");
        }

        [HttpPost, ValidateAntiForgeryToken, ModelValidationFilter]
        public async Task<IActionResult> DeleteSocialEvent(EventViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            await _socialEventService.DeleteSocialEvent(model.SocialEvent.SocialEventID);
            TempData["Message"] = $"Success! \"{model.SocialEvent.SocialEventName}\" has been Deleted!";
            Trace.TraceInformation($"{user.FirstName} {user.LastName} has deleted {model.SocialEvent.SocialEventName}");
            return RedirectToAction("UserEvents", "Event");

        }
    }
}
