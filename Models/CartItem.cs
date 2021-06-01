namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class CartItem : BaseModel
    {
        public User User { get; set; }

        public string GameId { get; set; }

        public Game GameInCart { get; set; }

        [Range(0, 30)]
        [Display(Name = "Quantity")]
        public int QuantityOfGame { get; set; }

        public decimal TotalGamePrice => GameInCart.Price * QuantityOfGame;
    }
}
