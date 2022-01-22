using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Klir.TechChallenge.Web.Api.Data;
using Klir.TechChallenge.Web.Api.DTOs;
using Klir.TechChallenge.Web.Api.Entities;
using Klir.TechChallenge.Web.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Klir.TechChallenge.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetProductsDtoAsync();

            if(products == null)
                return BadRequest("Was not possible to get the products");

            return Ok(products);
        }
    }
}