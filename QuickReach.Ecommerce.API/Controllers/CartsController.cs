using Microsoft.AspNetCore.Mvc;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickReach.Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository repository;
        private readonly IProductRepository productRepo;
        public CartsController(ICartRepository repository, IProductRepository productRepo)
        {
            this.repository = repository;
            this.productRepo = productRepo;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cart = this.repository.Retrieve(id);
            return Ok(cart);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cart newCart)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Create(newCart);

            return CreatedAtAction(nameof(this.Get), new { id = newCart.ID }, newCart);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cart cart)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Update(id, cart);

            return Ok(cart);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repository.Delete(id);

            return Ok();
        }

        [HttpPut("{cartId}/items/")]
        public IActionResult AddCartItem(int cartId, [FromBody] CartItem cartItem)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var cart = this.repository.Retrieve(cartId);

            if(cart == null)
            {
                return NotFound();
            }

            var product = this.productRepo.Retrieve(cartItem.ProductId);
            if(product == null)
            {
                return NotFound();
            }

            cart.AddCartItem(cartItem);

            this.repository.Update(cart.ID, cart);

            return Ok(cart);
        }

        // TODO:
        // Remove item from cartitem table
        [HttpPut("{cartId}/items/{cartItemId}")]
        public IActionResult RemoveCartItem(int cartId, int cartItemId)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var cart = this.repository.Retrieve(cartId);

            if (cart == null)
            {
                return NotFound();
            }

            cart.RemoveCartItem(cartItemId);

            this.repository.Update(cart.ID, cart);

            return Ok(cart);
        }
    }
}
