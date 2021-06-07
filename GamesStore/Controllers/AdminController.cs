namespace GamesStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Constants;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Model;
    using Model.ViewModels;
    using Model.ViewModels.Role;

    [Authorize(Roles = Role.ADMIN_ROLE)]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<User> userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = createRoleViewModel.RoleName
                };

                IdentityResult identityResult = await this.roleManager.CreateAsync(identityRole);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("LoadAllRoles", "Admin");
                }

                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(createRoleViewModel);
        }

        public IActionResult LoadAllRoles()
        {
            var roles = this.roleManager.Roles;

            return View(roles);
        }
        
        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = await this.roleManager.FindByIdAsync(id);

            if (role is null)
            {
                ViewBag.ErrorMessage = $"Role with id = {id} cannot be found!";

                return View("NotFound");
            }

            EditRoleViewModel editRoleViewModel = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (User user in await this.userManager.GetUsersInRoleAsync(role.Name))
            {
                editRoleViewModel.Users.Add(user.UserName);
            }

            return View(editRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            IdentityRole role = await this.roleManager.FindByIdAsync(editRoleViewModel.Id);

            if (role is null)
            {
                ViewBag.ErrorMessage = $"Role with id = {editRoleViewModel.Id} cannot be found!";

                return View("NotFound");
            }
            else
            {
                role.Name = editRoleViewModel.RoleName;
                var result = await this.roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("LoadAllRoles");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(editRoleViewModel);
        }

        public async Task<IActionResult> ChangeUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            IdentityRole role = await this.roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                ViewBag.ErrorMessage = $"Role with id = {roleId} cannot be found!";

                return View("NotFound");
            }

            List<UserRoleViewModel> userRoleViewModels = new List<UserRoleViewModel>();
            foreach (User user in this.userManager.Users)
            {
                UserRoleViewModel userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    Username = user.UserName
                };

                if (await this.userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                userRoleViewModels.Add(userRoleViewModel);
            }

            return View(userRoleViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUsersInRole(List<UserRoleViewModel> userRoleViewModels, string roleId)
        {
            IdentityRole role = await this.roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                ViewBag.ErrorMessage = $"Role with id = {roleId} cannot be found!";

                return View("NotFound");
            }

            for (int i = 0; i < userRoleViewModels.Count; i++)
            {
                User user = await this.userManager.FindByIdAsync(userRoleViewModels[i].UserId);

                IdentityResult identityResult = null;

                bool isAlreadyInRole = await this.userManager.IsInRoleAsync(user, role.Name);
                bool isEligibleToBeAddedInRole = userRoleViewModels[i].IsSelected && !isAlreadyInRole;
                bool isEligibleToBeRemovedFromRole = !userRoleViewModels[i].IsSelected && isAlreadyInRole;

                if (isEligibleToBeAddedInRole)
                {
                    identityResult = await this.userManager.AddToRoleAsync(user, role.Name);
                }
                else if (isEligibleToBeRemovedFromRole)
                {
                    identityResult = await this.userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (identityResult.Succeeded)
                {
                    if (i < (userRoleViewModels.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await this.roleManager.FindByIdAsync(id);

            if (role is null)
            {
                ViewBag.ErrorMessage = $"Role with id = {id} cannot be found!";

                return View("NotFound");
            }
            else
            {
                IdentityResult result = await this.roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("LoadAllRoles");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("LoadAllRoles");
            }
        }
    }
}
