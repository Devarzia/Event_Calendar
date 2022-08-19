using EventCalendar.Application;
using EventCalendar.Domain;
using EventCalendar.Filters;
using EventCalendar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventCalendar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        protected readonly RoleManager<ApplicationRole> _roleManager;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly IEventCategoryService _categoryService;
        protected readonly IContactService _contactService;
        protected readonly ILogService _logService;

        public AdminController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager,
            IEventCategoryService categoryService, IContactService contactService, ILogService logService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _categoryService = categoryService;
            _contactService = contactService;
            _logService = logService;
        }

        public IActionResult Index() => View();

        #region Manage Roles
        public IActionResult Roles()
        {
            var roles = _roleManager.Roles;
            var model = new AdminViewModel
            {
                Roles = roles.ToList()
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, ModelValidationFilter]
        public async Task<IActionResult> AddRole(AdminViewModel model)
        {
            var claim = new Claim(ClaimTypes.Role, model.Role.Name);
            await _roleManager.CreateAsync(model.Role);
            await _roleManager.AddClaimAsync(model.Role, claim);
            TempData["Message"] = $"Success! \"{model.Role.Name}\" has been successfully added";
            return RedirectToAction("Roles", "Admin");
        }

        public async Task<JsonResult> GetRoleByID(int roleID)
        {
            var role = await _roleManager.FindByIdAsync(roleID.ToString());
            return new JsonResult(role);
        }

        [HttpPost, ValidateAntiForgeryToken, ModelValidationFilter]
        public async Task<IActionResult> EditRole(AdminViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Role.Id.ToString());

            if (role != null)
            {
                role.Name = model.Role.Name;
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
            }

            await _roleManager.UpdateAsync(role);
            TempData["Message"] = $"Success! \"{model.Role.Name}\" has been successfully edited";
            return RedirectToAction("Roles", "Admin");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(AdminViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Role.Id.ToString());

            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
                TempData["Message"] = $"Success! \"{role.Name}\" has been successfully deleted";
                return RedirectToAction("Roles", "Admin");
            }

            return View(model);
        }
        #endregion

        #region Manage Categories
        public async Task<IActionResult> Categories()
        {
            var categories = await _categoryService.GetAllCategories();
            var model = new AdminViewModel
            {
                Categories = categories
            };

            return View(model);
        }

        public async Task<JsonResult> GetCategoryByID(int categoryID)
        {
            var category = await _categoryService.GetCategoryByID(categoryID);
            return new JsonResult(category);
        }

        [HttpPost, ValidateAntiForgeryToken, ModelValidationFilter]
        public async Task<IActionResult> AddCategory(AdminViewModel model)
        {
            await _categoryService.AddEventCategory(model.Category);
            TempData["Message"] = $"Success! \"{model.Category.EventCategoryName}\" has been successfully added";
            return RedirectToAction("Categories", "Admin");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(AdminViewModel model)
        {
            await _categoryService.EditEventCategory(model.Category);
            TempData["Message"] = $"Success! \"{model.Category.EventCategoryName}\" has been successfully edited";
            return RedirectToAction("Categories", "Admin");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(AdminViewModel model)
        {
            await _categoryService.DeleteEventCategory(model.Category.EventCategoryID);
            TempData["Message"] = $"Success! \"{model.Category.EventCategoryName}\" has been successfully deleted";
            return RedirectToAction("Categories", "Admin");
        }
        #endregion

        #region Manage User Roles
        public async Task<IActionResult> UserRoles()
        {
            var model = new AdminViewModel();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userViewModel = new UserViewModel
                {
                    UserID = user.Id,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email ?? "Not Registered",
                    PhoneNumber = user.PhoneNumber ?? "Not Registered",
                    Roles = await GetUserRoles(user)
                };

                model.Users.Add(userViewModel);
            }

            return View(model);
        }

        public async Task<JsonResult> AddRoleToUser(int userID)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userID);
            var roles = await GetFilteredRoles(user);

            var userViewModel = new UserViewModel
            {
                UserID = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email ?? "Not Registered",
                PhoneNumber = user.PhoneNumber ?? "Not Registered",
                Roles = await GetUserRoles(user)
            };

            var model = new AdminViewModel
            {
                Roles = roles,
                User = userViewModel
            };

            return new JsonResult(model);
        }

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(AdminViewModel model)
        {
            var selectedRole = await _roleManager.FindByIdAsync(model.Role.Id.ToString());
            var user = _userManager.Users.FirstOrDefault(x => x.Id == model.User.UserID);
            await _userManager.AddToRoleAsync(user, selectedRole.Name);
            TempData["Message"] = $"Success! The \"{selectedRole.Name}\" role has been successfully added to {user.FirstName} {user.LastName}";
            return RedirectToAction("UserRoles", "Admin");
        }

        public async Task<JsonResult> DeleteRoleFromUser(int userID)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userID);

            var userViewModel = new UserViewModel
            {
                UserID = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email ?? "Not Registered",
                PhoneNumber = user.PhoneNumber ?? "Not Registered",
                Roles = await GetUserRoles(user)
            };

            var roles = _roleManager.Roles.Where(x => userViewModel.Roles.Any(y => y == x.Name)).ToList();

            var model = new AdminViewModel
            {
                Roles = roles,
                User = userViewModel
            };

            return new JsonResult(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoleFromUser(AdminViewModel model)
        {
            var selectedRole = await _roleManager.FindByIdAsync(model.Role.Id.ToString());
            var user = _userManager.Users.FirstOrDefault(x => x.Id == model.User.UserID);
            await _userManager.RemoveFromRoleAsync(user, selectedRole.Name);
            TempData["Message"] = $"Success! The \"{selectedRole.Name}\" has been successfully remove from {user.FirstName} {user.LastName}";
            return RedirectToAction("UserRoles", "Admin");
        }
        #endregion

        #region Comments
        public async Task<IActionResult> Comments()
        {
            var contacts = await _contactService.GetContactInformationList();
            var model = new AdminViewModel
            {
                Contacts = contacts
            };

            return View(model);
        }

        public IActionResult SendMessage()
        {
            return View();
        }
        #endregion

        #region Logs
        public async Task<IActionResult> Logs()
        {
            var logs = await _logService.GetLogs();

            var model = new AdminViewModel
            {
                Logs = logs
            };

            return View(model);
        }
        #endregion

        #region Private Methods
        private async Task<IList<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        private async Task<List<ApplicationRole>> GetFilteredRoles(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles.Where(x => !userRoles.Any(y => y == x.Name)).ToList();

            if (roles.Count == 0)
                return roles;

            return roles;
        }
        #endregion
    }
}
