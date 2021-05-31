namespace GamesStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Game : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        public string Platform { get; set; }

        [Url]
        [Display(Name = "Image URL")]
        public string ImgUrl { get; set; }

        [Required]
        [MaxLength(30)]
        public string Developer { get; set; }

        [Display(Name = "PEGI rating")]
        public byte AgeRating { get; set; }

        public string Description { get; set; }

        [Display(Name = "Year of release")]
        public short YearOfRelease { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
