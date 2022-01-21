using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Klir.TechChallenge.Web.Api.Abstracts;
using Klir.TechChallenge.Web.Api.Entities;

namespace Klir.TechChallenge.Web.Api.Services.Cart
{
    public class NormalCartProductService : CartProductService
    {
        public NormalCartProductService(Product product, int quantidy = 1) : base(product, quantidy)
        {
        }

        protected override decimal CalculateTotalPrice()
        {
            return Product.Price * Quantidy;
        }
    }
}