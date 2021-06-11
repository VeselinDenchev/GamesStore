namespace Model.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CartItemViewModel : BaseModel
    {
        public GameViewModel GameInCart { get; set; }

        public string GameId => GameInCart.Id;

        [Range(0, 30)]
        [Display(Name = "Quantity")]
        public int QuantityOfGame { get; set; }

        public decimal TotalGamePrice => GameInCart.Price * QuantityOfGame;
    }
}
