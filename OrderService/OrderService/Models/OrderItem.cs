namespace ProductService.Models
{
    public class OrderItem
    {
        public string ProductId { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }
    }
}
