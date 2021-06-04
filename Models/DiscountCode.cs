namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Model.Interfaces;

    public class DiscountCode : BaseModel, IModificationTimestamp
    {
        public DiscountCode()
            : base()
        {
            this.ModifiedAtUtc = this.CreatedAtUtc;
        }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        public byte DiscountPercantage { get; set; }

        [ReadOnly(true)]
        public DateTime ModifiedAtUtc { get; set; }
    }
}
