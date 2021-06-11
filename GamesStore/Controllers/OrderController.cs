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
        private readonly UserManager<User> userManager;
        private string userId;
        private readonly DiscountCodeService discountCodeService;
        private List<DiscountCodeViewModel> discountCodes;
        private readonly OrderService orderService;

        public OrderController(UserManager<User> userManager, DiscountCodeService discountCodeService, OrderService orderService)
        {
            this.userManager = userManager;
            this.discountCodeService = discountCodeService;
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            bool sessionKeyCartIsNotNull = HttpContext.Session.GetString(Session.SESSION_KEY_CART) != null;

            if (sessionKeyCartIsNotNull)
            {
                List<CartItemViewModel> cartItemViewModels = DeserializeCart();
                ViewBag.cart = cartItemViewModels;

                List<CartItem> cart = new List<CartItem>();

                foreach (CartItemViewModel cartItemViewModel in cartItemViewModels)
                {
                    CartItem cartItem = this.orderService.PassDataFromViewModelToModelCartItem(cartItemViewModel);
                    cart.Add(cartItem);
                }

                bool sessionKeyTotalIsNotNull = HttpContext.Session.GetString(Session.SESSION_KEY_TOTAL) != null;
                string totalPriceString = null;
                if (sessionKeyTotalIsNotNull)
                {
                    totalPriceString = DeserializeTotalPrice();
                    decimal totalPrice = Decimal.Parse(totalPriceString);
                    ViewBag.total = totalPrice;
                }
                else
                {
                    decimal totalPrice = cartItemViewModels.Sum(cartItem => cartItem.GameInCart.Price * cartItem.QuantityOfGame);
                    totalPriceString = totalPrice.ToString();
                    ViewBag.total = totalPrice;

                    SerializeTotalPrice(totalPriceString);
                }

                bool sessionKeyOrderIsNull = HttpContext.Session.GetString(Session.SESSION_KEY_ORDER) == null;
                if (sessionKeyOrderIsNull)
                {
                    OrderViewModel order = new OrderViewModel()
                    {
                        Cart = cart,
                        TotalPrice = ViewBag.total,
                    };

                    SerializeOrder(order);
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpdateTotal(string discountCodeEntered)
        {
            if (discountCodeEntered is null)
            {
                return RedirectToAction(ActionName.INDEX);
            }

            List<CartItemViewModel> cart = DeserializeCart();
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
                    SerializeDiscountCode(discountCode);

                    DiscountCode discountCodeModel = discountCodeService.PassDataFromViewModelToModel(discountCode);
                    OrderViewModel order = DeserializeOrder();
                    order.TotalPrice *= (100 - discountCode.DiscountPercentage) / 100m;
                    order.TotalPrice += Constant.DELIVERY_TAX;
                    ViewBag.total = order.TotalPrice ;
                    order.DiscountCode = discountCodeModel;
                    SerializeOrder(order);
                }
            }

            SerializeCart(cart);
            SerializeTotalPrice(totalPriceString);
            
            return RedirectToAction(ActionName.INDEX);
        }

        public async Task<IActionResult> ConfirmOrderDetails()
        {
            OrderViewModel order = DeserializeOrder();

            this.userId = this.userManager.GetUserId(HttpContext.User);

            User user = await this.userManager.FindByIdAsync(this.userId);
            ViewBag.user = user;

            order.User = user;

            SerializeOrder(order);

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmOrderDetails(string firstName, string lastName, string deliveryAddress, string phoneNumber)
        {
            OrderViewModel orderViewModel = DeserializeOrder();
            orderViewModel.FirstName = firstName;
            orderViewModel.LastName = lastName;
            orderViewModel.DeliveryAddress = deliveryAddress;
            orderViewModel.PhoneNumber = phoneNumber;

            if (ModelState.IsValid)
            {
                this.orderService.CreateOrder(orderViewModel);
                RemoveAllSessions();
            }

            return View(ViewName.ORDER_DETAILS, orderViewModel);
        }

        [Authorize(Roles = Role.USER_ROLE)]
        public IActionResult OrderDetails(string orderId)
        {
            OrderViewModel order = this.orderService.FindOrderById(orderId);

            return View(order);
        }

        private void SerializeCart(List<CartItemViewModel> cart)
        {
            HttpContext.Session.SetString(Session.SESSION_KEY_CART, JsonConvert.SerializeObject(cart));
        }

        private List<CartItemViewModel> DeserializeCart()
        {
            List<CartItemViewModel> cart = JsonConvert
                .DeserializeObject<List<CartItemViewModel>>(HttpContext.Session.GetString(Session.SESSION_KEY_CART));

            return cart;
        }

        private void SerializeTotalPrice(string totalPriceString)
        {
            HttpContext.Session.SetString(Session.SESSION_KEY_TOTAL, JsonConvert.SerializeObject(totalPriceString));
        }

        private string DeserializeTotalPrice()
        {
            string totalPriceString = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString(Session.SESSION_KEY_TOTAL));

            return totalPriceString;
        }

        private void SerializeDiscountCode(DiscountCodeViewModel discountCode)
        {
            HttpContext.Session.SetString(Session.SESSION_KEY_DISCOUNT_CODE, JsonConvert.SerializeObject(discountCode));
        }

        private DiscountCodeViewModel DeserializeDiscountCode()
        {
            DiscountCodeViewModel discountCode = JsonConvert
                .DeserializeObject<DiscountCodeViewModel>(HttpContext.Session.GetString(Session.SESSION_KEY_DISCOUNT_CODE));

            return discountCode;
        }

        private void SerializeOrder(OrderViewModel order)
        {
            HttpContext.Session.SetString(Session.SESSION_KEY_ORDER, JsonConvert.SerializeObject(order));
        }

        private OrderViewModel DeserializeOrder()
        {
            OrderViewModel order = JsonConvert
                .DeserializeObject<OrderViewModel>(HttpContext.Session.GetString(Session.SESSION_KEY_ORDER));

            return order;
        }

        private void RemoveAllSessions()
        {
            HttpContext.Session.Remove(Session.SESSION_KEY_CART);
            HttpContext.Session.Remove(Session.SESSION_KEY_DISCOUNT_CODE);
            HttpContext.Session.Remove(Session.SESSION_KEY_TOTAL);
            HttpContext.Session.Remove(Session.SESSION_KEY_ORDER);
        }
    }
}
