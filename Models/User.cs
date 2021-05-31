namespace GamesStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(2)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Delivery address")]
        public string DeliveryAddress { get; set; }

        public List<Order> Orders { get; set; }
    }
}
