using Angular_Ecommerce.Context;
using Angular_Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Angular_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly AppDbContext cartDbContext;

        public CartsController(AppDbContext cartDbContext)
        {
            this.cartDbContext = cartDbContext;
        }
        private List<Cart> cartItems = new List<Cart>
        {
            new Cart { Id = Guid.NewGuid(), Name ="Item 1",Price=5999,Quantity=2,Img="https://m.media-amazon.com/images/I/71mvRhwhvcL._SY625._SX._UX._SY._UY_.jpg" },
            new Cart{ Id = Guid.NewGuid(), Name="Item 2",Price=1999,Quantity=3,Img="https://m.media-amazon.com/images/I/71mvRhwhvcL._SY625._SX._UX._SY._UY_.jpg" }
        };

        [HttpPost("add")]
        public ActionResult<Cart> AddToCart([FromBody] Cart cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest("Invalid cart item data");
            }
            var existingCartItem = cartDbContext.carts.FirstOrDefault(c => c.Id == cartItem.Id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartItem.Quantity;
            }
            else
            {
                cartDbContext.carts.Add(cartItem);
            }
            cartDbContext.SaveChanges();

            if (existingCartItem != null)
            {
                return Ok(existingCartItem);
            }
            else
            {

                return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, cartItem);
            }
        }


        [HttpGet("{id}")]
        public ActionResult<Cart> GetCartItem(Guid id)
        {
            var cartItem = cartDbContext.carts.FirstOrDefault(c => c.Id == id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return Ok(cartItem);
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<Cart> DeleteFromCart(Guid id)
        {
            var cartItem = cartDbContext.carts.Find(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            cartDbContext.carts.Remove(cartItem);
            cartDbContext.SaveChanges();
            return Ok(cartItem);
        }


        [HttpPut("update-quantity/{id}")]
        public IActionResult UpdateProductQuantity(Guid id, [FromBody] Cart newQuantity)
        {
            var cartItem = cartDbContext.carts.Find(x => x.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = newQuantity.Quantity;
            cartDbContext.SaveChanges();
            return Ok(cartItem);
        }


        [HttpGet]
        public ActionResult<IEnumerable<Cart>> GetCart()
        {
            List<Cart> cart = cartDbContext.carts.ToList();
            return Ok(cart);
        }
    }
}

