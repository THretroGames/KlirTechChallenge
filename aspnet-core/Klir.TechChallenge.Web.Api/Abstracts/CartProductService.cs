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
        public Product Product { get; protected set; }
        public int Quantidy { get; protected set; }
        public decimal TotalPrice { get; protected set; }
        public string PromotionApplied { get; protected set; }

        public CartProductService(Product product, int quantidy = 1)
        {
            Product = product;
            Quantidy = quantidy;
            PromotionApplied = string.Empty;
            TotalPrice = CalculateTotalPrice();
        }

        protected abstract decimal CalculateTotalPrice();
    }
}