using System.Collections.Generic;
using Klir.TechChallenge.Web.Api.DTOs;

namespace Klir.TechChallenge.Web.Api.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<CartProductDto> CartProductsDto { get; set; }
        public int Quantidy { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Saved { get; set; }
    }
}