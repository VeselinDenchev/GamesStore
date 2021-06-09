namespace Model.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Constants;

    public class OrderViewModel : BaseModel
    {
        private decimal totalPrice = 0;

        public User User { get; set; }

        public List<CartItemViewModel> Cart { get; set; }

        [Display(Name = "Discount code")]
        public DiscountCode DiscountCode { get; set; }

        [Display(Name = "Total")]
        public decimal TotalPrice
        {
            get
            {
                return this.totalPrice;
            }
            set
            {
                decimal totalGamesPrice = 0;

                /*foreach (CartItem item in Cart)
                {
                    totalGamesPrice += item.TotalGamePrice;
                }*/

                if (Cart.Count > 0)
                {
                    totalGamesPrice += Constant.DELIVERY_TAX;
                }

                this.totalPrice = totalGamesPrice;
            }
        }

        [Required]
        [Display(Name = "Delivery address")]
        public string DeliveryAddress { get; set; }

        [Display(Name = "Delivery date")]
        public DateTime DeliveryDate { get; set; }
}
}

