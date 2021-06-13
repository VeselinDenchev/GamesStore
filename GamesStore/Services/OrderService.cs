namespace GamesStore.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Data;

    using Model;
    using Model.ViewModels;

    public class OrderService
    {
        private readonly GamesStoreDbContext dbContext;

        private readonly GameService gameService;

        public OrderService(GamesStoreDbContext dbContext, GameService gameService)
        {
            this.dbContext = dbContext;
            this.gameService = gameService;
        }

        public void CreateOrder(OrderViewModel order)
        {
            Order newOrder = PassDataFromViewModelToModelOrder(order);

            List<CartItem> cart = newOrder.Cart;

            foreach (CartItem cartItem in cart)
            {
                this.dbContext.Attach(cartItem.GameInCart);
                this.dbContext.CartItems.Add(cartItem);
            }

            foreach (CartItem cartItem in newOrder.Cart)
            {
                this.dbContext.Games.Find(cartItem.GameInCart.Id).Quantity -= cartItem.QuantityOfGame;
            }
            this.dbContext.Attach(newOrder);
            this.dbContext.Orders.Add(newOrder);

            this.dbContext.SaveChanges();
        }

        public OrderViewModel FindOrderById(string id)
        {
            Order order = this.dbContext.Orders.FirstOrDefault(order => order.Id == id);

            OrderViewModel orderViewModel = PassDataFromModelToViewModelOrder(order);

            return orderViewModel;
        }

        public CartItem PassDataFromViewModelToModelCartItem(CartItemViewModel viewModel)
        {
            GameViewModel gameInViewModelCart = this.gameService.FindGameById(viewModel.GameId);
            Game gameInCart = this.gameService.PassDataFromViewModelToModel(gameInViewModelCart);
            string gameId = gameInCart.Id;

            CartItem model = new CartItem()
            {
                GameId = gameInCart.Id,
                GameInCart = gameInCart,
                QuantityOfGame = viewModel.QuantityOfGame,
            };

            return model;
        }

        private OrderViewModel PassDataFromModelToViewModelOrder(Order model)
        {
            OrderViewModel viewModel = new OrderViewModel()
            {
                Id = model.Id,
                User = model.User,
                Cart = model.Cart,
                DiscountCode = model.DiscountCode,
                TotalPrice = model.TotalPrice,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DeliveryAddress = model.DeliveryAddress,
                PhoneNumber = model.PhoneNumber,
                CreatedAtUtc = model.CreatedAtUtc,
            };

            return viewModel;
        }

        private Order PassDataFromViewModelToModelOrder(OrderViewModel viewModel)
        {
            Order model = new Order()
            {
                Id = viewModel.Id,
                User = viewModel.User,
                Cart = viewModel.Cart,
                DiscountCode = viewModel.DiscountCode,
                TotalPrice = viewModel.TotalPrice,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                DeliveryAddress = viewModel.DeliveryAddress,
                PhoneNumber = viewModel.PhoneNumber,
                DeliveryDate = viewModel.DeliveryDate,
                CreatedAtUtc = viewModel.CreatedAtUtc,
            };

            return model;
        }
    }
}
