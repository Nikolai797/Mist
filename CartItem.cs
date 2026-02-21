namespace GameStore.Services
{
    public class CartItem
    {
        public int GameId { get; set; }
        public string GameTitle { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
    }
}