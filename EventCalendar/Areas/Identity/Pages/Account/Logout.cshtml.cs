using EventCalendar.Application;
using EventCalendar.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EventCalendar.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        protected readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly ILogService _logService;
        protected readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogService logService, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logService = logService;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            await _signInManager.SignOutAsync();
            await _logService.CreateUserLogoutLog("User logged out.");
            System.Diagnostics.Trace.TraceInformation($"{user.FirstName} {user.LastName} ({user.Email}) has logged out");

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
