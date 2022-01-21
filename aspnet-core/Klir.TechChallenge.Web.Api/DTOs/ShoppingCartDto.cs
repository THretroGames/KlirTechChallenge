using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Web.Api.DTOs
{
    public class ShoppingCartDto
    {
        public ShoppingCartDto()
        {
            CartProductDtos = new List<CartProductDto>();
        }

        //public int Id { get; set; }
        public List<CartProductDto> CartProductDtos { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Saved { get; set; }
        public int Quantidy { get; set; }
    }
}