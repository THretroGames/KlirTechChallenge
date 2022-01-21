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

        protected override void CalculateProduct()
        {
            decimal q = CartProduct.Quantidy / 2;
            int d = (int)Math.Floor(q);

            decimal discount = d * CartProduct.Product.Price;
            decimal totalPrice = CartProduct.Product.Price * CartProduct.Quantidy;
            decimal totalPriceWithDiscount = totalPrice - discount;

            if(CartProduct.Quantidy >= 2)
            {
                CartProduct.PromotionApplied = CartProduct.Product.Promotion.Name;
            }

            CartProduct.TotalPrice = totalPriceWithDiscount;
        }
    }
}