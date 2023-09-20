using Angular_Ecommerce.Context;
using Angular_Ecommerce.Migrations;
using Angular_Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product = Angular_Ecommerce.Models.Product;

namespace Angular_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
     
        private readonly AppDbContext appDbContext;

        public ProductsController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var cards = await appDbContext.Products.ToListAsync();
            return Ok(cards);
        }




        [HttpGet]

        [Route("id:guid")]
        [ActionName("GetProduct")]

        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {

            var card = await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (card != null)
            { return Ok(card); }
            return NotFound("Product not Found!");
        }
        [HttpPost("Addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            product.Id = Guid.NewGuid();
            await appDbContext.Products.AddAsync(product);
            await appDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), product.Id, product);
        }

        [HttpPut]
        [Route("id:guid")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] Product product)

        {
            var existingproduct = await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingproduct != null)
            {
                existingproduct.Name=product.Name;
                existingproduct.Description=product.Description;
                existingproduct.OriginalPrice = product.OriginalPrice;
                existingproduct.SellingPrice = product.SellingPrice;
                existingproduct.Img = product.Img;
                await appDbContext.SaveChangesAsync();
                return Ok(existingproduct); 
            }
            return NotFound("Product Not Found!");
        }



       [HttpDelete]
        [Route("id:guid")]
       

         public ActionResult<Product> DeleteProduct(Guid id)
        {
            Product product = appDbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            appDbContext.Products.Remove(product);
            appDbContext.SaveChanges();

            return product;
        }
    }
}

