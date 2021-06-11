﻿namespace GamesStore.Controllers
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
                List<CartItemViewModel> cart = DeserializeCart();
                //ViewBag.cart = cart;
                //ViewBag.total = cart.Sum(cartItem => cartItem.GameInCart.Price * cartItem.QuantityOfGame);
                return View(cart);
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

                bool isNotEnoughQuantityAvailable = !this.gameService.CheckIfGameQuantityIsEnough(game.Id, MIN_BUYABLE_QUANTITY_OF_GAME);

                if (isNotEnoughQuantityAvailable)
                {
                    ViewBag.errorMessage = $"Not enough quantity of {game.Name} is available in stock!";
                }

                SerializeCart(cart);
                //ViewBag.cart = cart;
                return View("Index", cart);
            }
            else
            {
                List<CartItemViewModel> cart = DeserializeCart();
                int index = Exists(id, cart);
                bool isNotFound = index == NOT_FOUND_INDEX;

                if (isNotFound)
                {
                    cart.Add(new CartItemViewModel
                    {
                        GameInCart = game,
                        QuantityOfGame = MIN_BUYABLE_QUANTITY_OF_GAME
                    });

                    bool isNotEnoughQuantityAvailable = !this.gameService.CheckIfGameQuantityIsEnough(game.Id, MIN_BUYABLE_QUANTITY_OF_GAME);

                    if (isNotEnoughQuantityAvailable)
                    {
                        ViewBag.errorMessage = $"Not enough quantity of {game.Name} is available in stock!";
                    }

                    SerializeCart(cart);
                    //ViewBag.cart = cart;
                    return View("Index", cart);
                }
                else
                {
                    cart[index].QuantityOfGame++;

                    int desiredQuantity = cart[index].QuantityOfGame;

                    bool isNotEnoughQuantityAvailable = !this.gameService.CheckIfGameQuantityIsEnough(cart[index].GameId, desiredQuantity);

                    if (isNotEnoughQuantityAvailable)
                    {
                        ViewBag.errorMessage = $"Not enough quantity of {game.Name} is available in stock!";
                    }
                }

                SerializeCart(cart);
                //ViewBag.cart = cart;
                return View("Index", cart);
            }
        }

        public IActionResult Remove(string id)
        {
            List<CartItemViewModel> cart = DeserializeCart();
            int index = Exists(id, cart);
            cart.RemoveAt(index);
            SerializeCart(cart);

            return RedirectToAction(INDEX_ACTION);
        }

        [HttpPost]
        public IActionResult UpdateGameQuantity(int[] quantity)
        {
            List<CartItemViewModel> cart = DeserializeCart();

            for (int i = 0; i < cart.Count; i++)
            {
                
                bool isEnoughQuantityAvailable = this.gameService.CheckIfGameQuantityIsEnough(cart[i].GameId, quantity[i]);

                if (isEnoughQuantityAvailable)
                {
                    cart[i].QuantityOfGame = quantity[i];
                }
                else
                {
                    GameViewModel game = this.gameService.FindGameById(cart[i].GameId);
                    ViewBag.errorMessage = $"Not enough quantity of {game.Name} is available in stock!";
                }
            }

            SerializeCart(cart);

            return View("Index", cart);
        }

        private void SerializeCart(List<CartItemViewModel> cart)
        {
            HttpContext.Session.SetString(SESSION_KEY, JsonConvert.SerializeObject(cart));
        }

        private List<CartItemViewModel> DeserializeCart()
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
