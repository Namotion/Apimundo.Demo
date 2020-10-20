namespace ProductService.Models
{
    public class Order
    {
        public string Id { get; set; }

        public OrderItem[] Items { get; set; }
    }
}
