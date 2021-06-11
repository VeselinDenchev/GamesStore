namespace Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CartItem : BaseModel
    {
        public string GameId { get; set; }

        public Game GameInCart { get; set; }

        [Range(0, 30)]
        [Display(Name = "Quantity")]
        public int QuantityOfGame { get; set; }

        public decimal TotalGamePrice => GameInCart.Price * QuantityOfGame;
    }
}
