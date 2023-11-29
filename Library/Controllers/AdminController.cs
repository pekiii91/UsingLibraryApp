using Library.InterfaceService;
using Library.Models;
using Library.Models.UsersHelperModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IAdminService adminService, UserManager<ApplicationUser> userManager)
        {
            _adminService = adminService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _adminService.GetUsers();
            return View(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddStudent(UserModel model)
        {

            var result = await _adminService.AddStudetRole(model.Id);
            return Json(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddLibrarian(UserModel model)
        {
            var result = await _adminService.AddLibrarinaRole(model.Id);
            return Json(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> DeleteRole(Guid id)
        {
            var result = await _adminService.RemoveRoles(id);
            return Json(result);
        }


    }
}
