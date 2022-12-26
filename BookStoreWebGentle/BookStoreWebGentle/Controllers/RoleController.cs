using BookStoreWebGentle.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
           this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");   
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);    
                }
            }
            return View(model);
            
        }

    }

}
