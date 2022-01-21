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

        protected override void CalculateProduct()
        {
            decimal q = CartProduct.Quantidy / 3;
            int d = (int)Math.Floor(q);

            decimal discount = d * 10m;
            decimal totalPrice = ((CartProduct.Quantidy - (d * 3)) * CartProduct.Product.Price);
            decimal totalPriceWithDiscount = totalPrice + discount;

            if(CartProduct.Quantidy >= 3)
            {
                CartProduct.PromotionApplied = CartProduct.Product.Promotion.Name;
            }

            CartProduct.TotalPrice = totalPriceWithDiscount;
        }
    }
}