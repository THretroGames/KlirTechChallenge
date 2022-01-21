using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Klir.TechChallenge.Web.Api.DTOs;
using Klir.TechChallenge.Web.Api.Entities;

namespace Klir.TechChallenge.Web.Api.Abstracts
{
    public abstract class CartProductService
    {
        public CartProduct CartProduct { get; protected set; }

        public CartProductService(Product product, int quantidy = 1)
        {
            CartProduct = new CartProduct();
            CartProduct.Product = product;
            CartProduct.Quantidy = quantidy;
            CartProduct.PromotionApplied = string.Empty;
            CalculateProduct();
            CalculateDiscount();
        }

        private void CalculateDiscount(){
            CartProduct.OriginalPrice = CartProduct.Product.Price * CartProduct.Quantidy;
            CartProduct.Saved = CartProduct.OriginalPrice - CartProduct.TotalPrice;
        }

        protected abstract void CalculateProduct();
    }
}