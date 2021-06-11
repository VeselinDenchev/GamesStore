namespace Model
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

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
        [Range(0, 100)]
        public sbyte DiscountPercentage { get; set; }

        [ReadOnly(true)]
        public DateTime ModifiedAtUtc { get; set; }
    }
}
