using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Klir.TechChallenge.Web.Api.Data;
using Klir.TechChallenge.Web.Api.DTOs;
using Klir.TechChallenge.Web.Api.Entities;
using Klir.TechChallenge.Web.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Klir.TechChallenge.Web.Api.Controllers
{
    [Route("[controller]")]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, IShoppingCartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<ShoppingCartDto>> UpdateCart([FromBody] IEnumerable<RequestCartProductDto> cartProducts)
        {
            if (cartProducts == null)
                return BadRequest("Invalid Parameters");

            var cart = await _cartService.GetShoppingCart(cartProducts);

            if (cart == null)
                return BadRequest("Was not possible to create the shopping cart");

            return Ok(cart);
        }
    }
}