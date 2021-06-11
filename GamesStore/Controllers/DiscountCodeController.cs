namespace GamesStore.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using GamesStore.Services;
    using Model.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Constants;

    public class DiscountCodeController : Controller
    {
        private readonly DiscountCodeService discountCodeService;

        public DiscountCodeController(DiscountCodeService discountCodeService)
        {
            this.discountCodeService = discountCodeService;
        }

        // GET: Game
        public IActionResult Index()
        {
            List<DiscountCodeViewModel> discountCodes = this.discountCodeService.LoadAllDiscountCodes();

            return View(discountCodes);
        }

        // GET: Game/Details
        public IActionResult Details(string id)
        {
            DiscountCodeViewModel discountCode = this.discountCodeService.CheckIfDiscountCodeIdIsValid(id);

            if (discountCode is null)
            {
                return NotFound();
            }

            return View(discountCode);
        }

        [Authorize(Roles = Role.ADMIN_ROLE)]
        // GET: Game/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DiscountCodeViewModel discountCode)
        {
            if (ModelState.IsValid)
            {
                this.discountCodeService.CreateDiscountCode(discountCode);

                return RedirectToAction(nameof(Index));
            }

            return View(discountCode);
        }


        [Authorize(Roles = Role.ADMIN_ROLE)]
        // GET: Game/Edit
        public IActionResult Edit(string id)
        {
            DiscountCodeViewModel discountCode = this.discountCodeService.CheckIfDiscountCodeIdIsValid(id);

            return View(discountCode);
        }

        // POST: Game/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DiscountCodeViewModel discountCode)
        {
            if (discountCode is null)
            {
                return NotFound();
            }

            this.discountCodeService.SaveEditedDiscountCode(discountCode);

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = Role.ADMIN_ROLE)]
        // GET: Game/Delete
        public IActionResult Delete(string id)
        {
            DiscountCodeViewModel discountCode = this.discountCodeService.CheckIfDiscountCodeIdIsValid(id);

            return View(discountCode);
        }

        // POST: Game/Delete
        [HttpPost, ActionName(ActionName.DELETE)]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            this.discountCodeService.DeleteDiscountCode(id);

            return RedirectToAction(nameof(Index));
        }

        private bool DiscountCodeExists(string id)
        {
            bool exists = this.discountCodeService.CheckIfDiscountCodeExists(id);

            return exists;
        }
    }
}
