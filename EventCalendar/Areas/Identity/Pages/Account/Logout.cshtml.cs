using EventCalendar.Application;
using EventCalendar.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EventCalendar.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        protected readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly ILogService _logService;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogService logService)
        {
            _signInManager = signInManager;
            _logService = logService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            await _logService.CreateUserLogoutLog("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
