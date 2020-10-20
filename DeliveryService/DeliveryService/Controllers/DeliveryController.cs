using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api")]
    public class DeliveryController : ControllerBase
    {
        [HttpGet("orders/{orderId}/delivery/status")]
        public DeliveryStatus GetDeliveryStatus(string orderId)
        {
            return new DeliveryStatus();
        }
    }
}
