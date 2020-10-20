using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using ProductService.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Product> FindProducts([NotNull]string query)
        {
            return new Product[0];
        }

        [HttpGet("{productId}")]
        public Product GetProduct(string productId)
        {
            return new Product();
        }
    }
}
