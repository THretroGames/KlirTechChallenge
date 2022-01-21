using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Klir.TechChallenge.Web.Api.Abstracts;
using Klir.TechChallenge.Web.Api.Entities;

namespace Klir.TechChallenge.Web.Api.Services.Cart
{
    public class Buy1Get1FreeCartProductService : CartProductService
    {
        public Buy1Get1FreeCartProductService(Product product, int quantidy = 1) : base(product, quantidy)
        {
        }

        protected override decimal CalculateTotalPrice()
        {
            decimal q = Quantidy / 2;
            int d = (int)Math.Floor(q);

            decimal discount = d * Product.Price;
            decimal totalPrice = Product.Price * Quantidy;
            decimal totalPriceWithDiscount = totalPrice - discount;

            if(Quantidy >= 2)
            {
                PromotionApplied = Product.Promotion.Name;
            }

            return totalPriceWithDiscount;
        }
    }
}