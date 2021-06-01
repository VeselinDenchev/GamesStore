namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using Model.Interfaces;

    public class User : IdentityUser, ICreationTimestamp
    {
        public User()
            : base()
        {
            this.CreatedAtUtc = DateTime.UtcNow;
        }

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

        public DateTime CreatedAtUtc { get; set; }
    }
}
