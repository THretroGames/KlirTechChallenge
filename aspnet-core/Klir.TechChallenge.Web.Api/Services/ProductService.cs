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
    }
}