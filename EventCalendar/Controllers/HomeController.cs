using EventCalendar.Application;
using EventCalendar.Filters;
using EventCalendar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventCalendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;

        public HomeController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(HomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _contactService.SubmitContactInformation(model.Contact);
                TempData["message"] = "Success! We will contact you shortly";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
