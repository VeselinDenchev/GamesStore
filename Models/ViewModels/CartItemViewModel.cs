﻿namespace Model.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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