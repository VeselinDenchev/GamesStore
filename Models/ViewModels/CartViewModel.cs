namespace Model.ViewModels
{
    public class CartViewModel
    {
        public string GameId { get; set; }

        public string GameName { get; set; }

        public string ImgUrl { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
