using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Practice.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml.Linq;

namespace API_Practice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Laptop", Category = "Electronics", Price = 10000 },
            new Product { ProductId = 2, ProductName = "Mouse", Category = "Electronics", Price = 1000 },
            new Product { ProductId = 3, ProductName = "Keyboard", Category = "Electronics", Price = 800 }
        };
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_products);
        }

        [HttpGet("{productId}")]
        public ActionResult<Product> GetProduct(int productId)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with id {productId} is not found" });
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult PostProduct([FromBody] Product product)
        {
            product.ProductId = _products.Max(x => x.ProductId) + 1;
            _products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { productId = product.ProductId }, product);
        }

        [HttpPut("{productId}")]
        public ActionResult PutProduct(int productId, [FromBody] Product updateproduct)
        {
            if (productId != updateproduct.ProductId)
            {
                return BadRequest();
            }
            var existingproduct = _products.FirstOrDefault(x => x.ProductId == productId);
            if (existingproduct == null)
            {
                return NotFound();
            }
            existingproduct.ProductName = updateproduct.ProductName; existingproduct.Category = updateproduct.Category;
            existingproduct.Price = updateproduct.Price;

            return NoContent();
        }

        [HttpDelete("{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);
            if (product == null)
            {
                return NotFound();
            }
            _products.Remove(product);
            return NoContent();
        }

        [HttpGet("price/{price}/Category/{category}")]
        public ActionResult<IEnumerable<Product>> GetProductByPriceCat (int price, string category)
        {
            var filterdata = _products
                .Where(p => p.Price == price
                && p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

            if(filterdata.Count == 0)
            {
                return NotFound();
            }
            return Ok(filterdata);
        }

        [HttpGet("Search")]
        public ActionResult<IEnumerable<Product>> SearchProducts([FromQuery] string? category, [FromQuery] string? name) {
            /*var filtereddata = _products
            .Where(p->p.Category. Equals(category, StringComparison. OrdinalIgnoreCase)).ToList();*/

            var filtereddata = _products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
                filtereddata = filtereddata
                    .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));


            if (!string.IsNullOrEmpty(name))
                filtereddata = filtereddata
                    .Where(p => p.ProductName.Equals(name, StringComparison.OrdinalIgnoreCase));


            var result = filtereddata.ToList();

            if (!result.Any())
                return NotFound();

            return Ok(result);
        }
    }
}

