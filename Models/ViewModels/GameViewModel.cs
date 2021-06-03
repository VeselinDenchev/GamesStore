using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class GameViewModel : BaseModel
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

        [Range(3, 18)]
        [Display(Name = "PEGI rating")]
        public byte AgeRating { get; set; }

        public string Description { get; set; }

        [Range(1960, 2021)]
        [Display(Name = "Year of release")]
        public short YearOfRelease { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
