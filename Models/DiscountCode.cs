namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class DiscountCode : BaseModel
    {
        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        public byte DiscountPercantage { get; set; }
    }
}
