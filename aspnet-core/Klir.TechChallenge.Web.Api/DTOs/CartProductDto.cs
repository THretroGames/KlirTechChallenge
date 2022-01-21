using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Web.Api.DTOs
{
    public class CartProductDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantidy { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string PromotionApplied { get; set; }
    }
}