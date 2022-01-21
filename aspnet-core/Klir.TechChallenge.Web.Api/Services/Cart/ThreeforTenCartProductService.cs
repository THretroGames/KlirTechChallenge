using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Klir.TechChallenge.Web.Api.Abstracts;
using Klir.TechChallenge.Web.Api.Entities;

namespace Klir.TechChallenge.Web.Api.Services.Cart
{
    public class ThreeforTenCartProductService : CartProductService
    {
        public ThreeforTenCartProductService(Product product, int quantidy = 1) : base(product, quantidy)
        {
        }

        protected override decimal CalculateTotalPrice()
        {
            decimal q = Quantidy / 3;
            int d = (int)Math.Floor(q);

            decimal discount = d * 10m;
            decimal totalPrice = ((Quantidy - (d * 3)) * Product.Price);
            decimal totalPriceWithDiscount = totalPrice + discount;

            if(Quantidy >= 3)
            {
                PromotionApplied = Product.Promotion.Name;
            }

            return totalPriceWithDiscount;
        }
    }
}