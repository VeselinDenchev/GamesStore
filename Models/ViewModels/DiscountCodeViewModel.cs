namespace Model.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Model.Interfaces;

    public class DiscountCodeViewModel : BaseModel, IModificationTimestamp
    {
        public DiscountCodeViewModel()
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
