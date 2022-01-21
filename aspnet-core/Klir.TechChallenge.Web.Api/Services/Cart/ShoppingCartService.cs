using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Klir.TechChallenge.Web.Api.Abstracts;
using Klir.TechChallenge.Web.Api.DTOs;
using Klir.TechChallenge.Web.Api.Entities;
using Klir.TechChallenge.Web.Api.Interfaces;

namespace Klir.TechChallenge.Web.Api.Services.Cart
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IProductService _productService;

        ShoppingCartDto ShoppingCartDto = new ShoppingCartDto();

        private readonly IMapper _mapper;

        public ShoppingCartService(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
            ShoppingCartDto.TotalPrice = 0m;
        }

        public async Task<ShoppingCartDto> GetShoppingCart(IEnumerable<CartProductDto> cartProdutcs)
        {
            await CalculateCart(cartProdutcs.ToList());
            return ShoppingCartDto;
        }

        public async Task<bool> CalculateCart(List<CartProductDto> cartProdutcs)
        {
            ShoppingCartDto cartDto = new ShoppingCartDto();

            foreach (CartProductDto c in cartProdutcs)
            {
                Product prod = await _productService.GetProductAsync(c.ProductId);
                CartProductService cps = GetProductService(prod, c.Quantidy);
                CartProductDto cartProductDto = GetCartProductDto(cps);
                ShoppingCartDto.Quantidy += cps.CartProduct.Quantidy;
                ShoppingCartDto.CartProductDtos.Add(cartProductDto);
                ShoppingCartDto.TotalPrice += cps.CartProduct.TotalPrice;
                ShoppingCartDto.OriginalPrice += cps.CartProduct.OriginalPrice;
                ShoppingCartDto.Saved += cps.CartProduct.Saved;
            }
            return true;
        }

        private CartProductService GetProductService(Product product, int quantidy = 1)
        {
            if(product.Promotion == null)
                return new NormalCartProductService(product, quantidy);

            switch (product.Promotion.TypeId)
            {
                case 1:
                    return new Buy1Get1FreeCartProductService(product, quantidy);
                case 2:
                    return new ThreeforTenCartProductService(product, quantidy);
                default:
                    return new NormalCartProductService(product, quantidy);
            }
        }

        public CartProductDto GetCartProductDto(CartProductService cartProductService)
        {
            CartProductDto cartProductDto = new CartProductDto();
            cartProductDto = _mapper.Map<CartProductDto>(cartProductService.CartProduct);
            return cartProductDto;
        }

        // protected void UpdateTotalPrice(List<CartProduct> CartProducts)
        // {
        //     foreach (CartProduct c in CartProducts)
        //     {
        //         TotalPrice += c.TotalPrice;
        //     }
        // }
    }
}