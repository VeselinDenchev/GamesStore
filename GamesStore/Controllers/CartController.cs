namespace GamesStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GamesStore.Services;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Model;
    using Model.ViewModels;

    using Newtonsoft.Json;

    public class CartController : Controller
    {
        private const string SESSION_KEY = "cart";
        private const string INDEX_ACTION = "Index";
        private const string GAME_CONTROLLER = "Game";

        private const int MIN_BUYABLE_QUANTITY_OF_GAME = 1;
        private const int NOT_FOUND_INDEX = -1;

        private readonly GameService gameService;

        public CartController(GameService service)
        {
            this.gameService = service;
        }

        public IActionResult Index()
        {
            bool sessionKeyIsNotNull = HttpContext.Session.GetString(SESSION_KEY) != null;

            if (sessionKeyIsNotNull)
            {
                List<CartItemViewModel> cart = DeserializeObject();
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(cartItem => cartItem.GameInCart.Price * cartItem.QuantityOfGame);
            }

            return View();

        }

        public IActionResult Buy(string id)
        {
            GameViewModel game = gameService.FindGameById(id);

            bool sessionKeyIsNull = HttpContext.Session.GetString(SESSION_KEY) == null;

            if (sessionKeyIsNull)
            {
                List<CartItemViewModel> cart = new List<CartItemViewModel>
                {
                    new CartItemViewModel
                    {
                        GameInCart = game,
                        QuantityOfGame = MIN_BUYABLE_QUANTITY_OF_GAME
                    }
                };

                SerializeObject(cart);
            }
            else
            {
                List<CartItemViewModel> cart = DeserializeObject();
                int index = Exists(id, cart);
                bool isNotFound = index == NOT_FOUND_INDEX;

                if (isNotFound)
                {
                    cart.Add(new CartItemViewModel
                    {
                        GameInCart = game,
                        QuantityOfGame = MIN_BUYABLE_QUANTITY_OF_GAME
                    });
                }
                else
                {
                    cart[index].QuantityOfGame++;
                }

                SerializeObject(cart);
            }

            return RedirectToAction(INDEX_ACTION);
        }

        public IActionResult Remove(string id)
        {
            List<CartItemViewModel> cart = DeserializeObject();
            int index = Exists(id, cart);
            cart.RemoveAt(index);
            SerializeObject(cart);

            return RedirectToAction(INDEX_ACTION);
        }

        [HttpPost]
        public IActionResult UpdateGameQuantity(int[] quantity)
        {
            List<CartItemViewModel> cart = DeserializeObject();

            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].QuantityOfGame = quantity[i];
            }

            SerializeObject(cart);

            return RedirectToAction(INDEX_ACTION);
        }

        /*public IActionResult Checkout()
        {
            HttpContext.Session.Remove(SESSION_KEY);

            return RedirectToAction(INDEX_ACTION, "Order");
        }*/

        private void SerializeObject(List<CartItemViewModel> cart)
        {
            HttpContext.Session.SetString(SESSION_KEY, JsonConvert.SerializeObject(cart));
        }

        private List<CartItemViewModel> DeserializeObject()
        {
            List<CartItemViewModel> cart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(HttpContext.Session.GetString(SESSION_KEY));

            return cart;
        }

        private int Exists(string id, List<CartItemViewModel> cart)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].GameInCart.Id == id)
                {
                    return i;
                }
            }

            return NOT_FOUND_INDEX;
        }
    }
}
