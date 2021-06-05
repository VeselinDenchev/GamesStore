namespace GamesStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Model;
    using GamesStore.Services;
    using Model.ViewModels;

    public class DiscountCodeController : Controller
    {
        private readonly DiscountCodeService discountCodeService;

        public DiscountCodeController(DiscountCodeService service)
        {
            discountCodeService = service;
        }

        // GET: DiscountCode
        public IActionResult Index()
        {
            List<DiscountCodeViewModel> discountCodes = discountCodeService.LoadAllDiscountCodes();

            return View(discountCodes);
        }
        /*
        // GET: DiscountCode/Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountCode = await discountCodeService.DiscountCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discountCode == null)
            {
                return NotFound();
            }

            return View(discountCode);
        }*/

        // GET: DiscountCode/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiscountCode/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DiscountCodeViewModel discountCode)
        {
            if (ModelState.IsValid)
            {
                discountCodeService.CreateDiscountCode(discountCode);

                return RedirectToAction(nameof(Index));
            }

            return View(discountCode);
        }

        // GET: DiscountCode/Edit
        public IActionResult Edit(string id)
        {
            DiscountCodeViewModel discountCode = discountCodeService.CheckIfDiscountCodeIdIsValid(id);

            return View(discountCode);
        }

        // POST: DiscountCode/Edit
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

            discountCodeService.SaveEditedDiscountCode(discountCode);

            return RedirectToAction(nameof(Index));
        }

        // GET: DiscountCode/Delete
        public IActionResult Delete(string id)
        {
            DiscountCodeViewModel discountCode = discountCodeService.CheckIfDiscountCodeIdIsValid(id);

            return View(discountCode);
        }

        // POST: DiscountCode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(DiscountCodeViewModel discountCode)
        {
            discountCodeService.DeleteDiscountCode(discountCode);

            return RedirectToAction(nameof(Index));
        }
    }
}
