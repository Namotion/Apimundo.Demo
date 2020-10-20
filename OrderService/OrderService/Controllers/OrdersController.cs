using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            return new Order[0];
        }
    }
}
