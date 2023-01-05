using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Controllers
{
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [Authorize(Roles ="Ashok,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        [Authorize(Roles = "Ashok,SuperAdmin")]

        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[Authorize(Roles = "SuperAdmin")]
        //public async Task<IActionResult> DeleteRole(string roleId)
        //{
        //    var role = await _roleManager.FindByIdAsync(roleId);
        //    if (role == null)
        //    {
        //        ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
        //        return View("Index");
        //    }
        //    else
        //    {
        //        var result = await _roleManager.DeleteAsync(role);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "UserRoles");
        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //        return View("Index", "RoleManager");
        //    }
        //}
    }
}
