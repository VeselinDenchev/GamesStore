namespace GamesStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Constants;

    using GamesStore.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Model;
    using Model.ViewModels;

    using Newtonsoft.Json;

    [Authorize]
    public class OrderController : Controller
    {
        private const string SESSION_KEY_CART = "cart";
        private const string SESSION_KEY_TOTAL = "total";
        private const string INDEX_ACTION = "Index";
        private const string GAME_CONTROLLER = "Game";

        private readonly UserManager<User> userManager;
        private string userId;
        private readonly DiscountCodeService discountCodeService;
        private List<DiscountCodeViewModel> discountCodes;

        public OrderController(UserManager<User> userManager, DiscountCodeService discountCodeService)
        {
            this.userManager = userManager;
            this.discountCodeService = discountCodeService;
        }

        public async Task<IActionResult> Index()
        {
            userId = this.userManager.GetUserId(HttpContext.User);

            User user = await this.userManager.FindByIdAsync(this.userId);
            ViewBag.user = user;

            //ViewBag.userId = this.userManager.GetUserId(HttpContext.User);

            bool sessionKeyCartIsNotNull = HttpContext.Session.GetString(SESSION_KEY_CART) != null;

            if (sessionKeyCartIsNotNull)
            {
                List<CartItemViewModel> cart = DeserializeObject();
                ViewBag.cart = cart;

                bool sessionKeyTotalIsNotNull = HttpContext.Session.GetString(SESSION_KEY_TOTAL) != null;
                string totalPriceString = null;
                if (sessionKeyTotalIsNotNull)
                {
                    totalPriceString = DeserializeTotalPrice();
                    decimal totalPrice = Decimal.Parse(totalPriceString);
                    ViewBag.total = totalPrice;
                }
                else
                {
                    decimal totalPrice = cart.Sum(cartItem => cartItem.GameInCart.Price * cartItem.QuantityOfGame);
                    totalPriceString = totalPrice.ToString();
                    ViewBag.total = totalPrice;

                    SerializeTotalPrice(totalPriceString);
                }
                
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateTotal(string discountCodeEntered)
        {
            if (discountCodeEntered is null)
            {
                return RedirectToAction(INDEX_ACTION);
            }

            List<CartItemViewModel> cart = DeserializeObject();
            string totalPriceString = DeserializeTotalPrice();
            decimal totalPrice = Decimal.Parse(totalPriceString);

            this.discountCodes = this.discountCodeService.LoadAllDiscountCodes();

            foreach (DiscountCodeViewModel discountCode in this.discountCodes)
            {
                bool isValid = discountCodeEntered.ToUpper() == discountCode.Code;

                if (isValid)
                {
                    totalPrice *= (100 - discountCode.DiscountPercentage) / 100m;
                    totalPriceString = totalPrice.ToString();
                }
            }

            SerializeObject(cart);
            SerializeTotalPrice(totalPriceString);

            return RedirectToAction(INDEX_ACTION);
        }

        private void SerializeObject(List<CartItemViewModel> cart)
        {
            HttpContext.Session.SetString(SESSION_KEY_CART, JsonConvert.SerializeObject(cart));
        }

        private List<CartItemViewModel> DeserializeObject()
        {
            List<CartItemViewModel> cart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(HttpContext.Session.GetString(SESSION_KEY_CART));

            return cart;
        }

        private void SerializeTotalPrice(string totalPriceString)
        {
            HttpContext.Session.SetString(SESSION_KEY_TOTAL, JsonConvert.SerializeObject(totalPriceString));
        }

        private string DeserializeTotalPrice()
        {
            string totalPriceString = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString(SESSION_KEY_TOTAL));

            return totalPriceString;
        }
    }
}
