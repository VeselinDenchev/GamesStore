namespace Model.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class ShoppingCartViewModel
    {
        public string ItemId { get; set; }

        public string ItemName { get; set; }

        public string ImgUrl { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
