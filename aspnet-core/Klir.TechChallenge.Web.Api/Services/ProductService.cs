using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Klir.TechChallenge.Web.Api.DTOs;
using Klir.TechChallenge.Web.Api.Entities;
using Klir.TechChallenge.Web.Api.Interfaces;

namespace Klir.TechChallenge.Web.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        public IMapper _mapper { get; }

        public ProductService(IProductRepository productRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsDtoAsync()
        {
            var products = await GetProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepo.GetProductsAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _productRepo.GetProductAsync(id);
        }




        //only for test
        public IEnumerable<ProductDto> GetRandomProductsDto()
        {
            List<ProductDto> products = new List<ProductDto>();
            List<string> promotions = new List<string>();
            promotions.Add("");
            promotions.Add("Buy 1 Get 1 Free");
            promotions.Add("3 for 10 Euro");
            int quantity = new Random().Next(5, 21);
            for (int i = 0; i < quantity; i++)
            {
                ProductDto p = new ProductDto();
                p.Id = i;
                p.Name = "Product " + i;
                p.Price = new Random().Next(2, 51);
                p.Promotion = promotions[new Random().Next(0, 3)];
                products.Add(p);
            }
            return products;
        }
    }
}