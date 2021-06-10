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

        public List<CartItem> Cart { get; set; }

        [Display(Name = "Discount code")]
        public DiscountCode DiscountCode { get; set; }

        [Display(Name = "Total")]
        public decimal TotalPrice { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Delivery address")]
        public string DeliveryAddress { get; set; }

        [MinLength(10)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Delivery date")]
        public DateTime DeliveryDate => CreatedAtUtc.AddDays(3);
}
}

