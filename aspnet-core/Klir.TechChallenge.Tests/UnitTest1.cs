using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Klir.TechChallenge.Web.Api.Abstracts;
using Klir.TechChallenge.Web.Api.Data;
using Klir.TechChallenge.Web.Api.DTOs;
using Klir.TechChallenge.Web.Api.Entities;
using Klir.TechChallenge.Web.Api.Helpers;
using Klir.TechChallenge.Web.Api.Interfaces;
using Klir.TechChallenge.Web.Api.Services;
using Klir.TechChallenge.Web.Api.Services.Cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KlirTechChallenge.Tests
{
    public class UnitTest1
    {
        private readonly IProductRepository ProductRepository;
        private readonly IProductService ProductService;
        private readonly ServiceCollection Services = new ServiceCollection();
        private readonly ServiceProvider ServiceProvider;

        public UnitTest1()
        {
            string startupPath = Environment.CurrentDirectory;
            Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite("Data source=" + startupPath + "/KlirTech.db");
            });
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            Services.AddScoped<IShoppingCartService, ShoppingCartService>();
            Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            ServiceProvider = Services.BuildServiceProvider();
        }

        [Fact]
        public async Task GetShoppingCart_WithEmptyProducts_ReturnEmptyCart()
        {
            //Arrange
            var cartService = ServiceProvider.GetService<IShoppingCartService>();
            IEnumerable<RequestCartProductDto> prods = Enumerable.Empty<RequestCartProductDto>();

            //Act
            var r = await cartService.GetShoppingCart(prods);

            //Assert
            Assert.Empty(r.CartProductDtos);
        }

        [Fact]
        public async Task GetShoppingCart_WithOneProducts_ReturnOneProductCart()
        {
            //Arrange
            var cartService = ServiceProvider.GetService<IShoppingCartService>();

            RequestCartProductDto cart = new RequestCartProductDto();
            cart.ProductId = 1;
            cart.Quantidy = 3;

            IEnumerable<RequestCartProductDto> prods = new List<RequestCartProductDto>() { cart };

            //Act
            var r = await cartService.GetShoppingCart(prods);

            //Assert
            Assert.Equal(r.CartProductDtos.Count, prods.Count());
        }

        [Fact]
        public async Task GetProduct_InvalidId_ReturnNull()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();

            //Act
            var r = await prodService.GetProductAsync(-1);

            //Assert
            Assert.Equal(r, null);
        }

        [Fact]
        public async Task GetProduct_ValidId_ReturnProduct()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();

            //Act
            var r = await prodService.GetProductAsync(1);

            //Assert
            Assert.Equal(r.Id, 1);
        }

        [Fact]
        public async Task GetProducts_Valid_ReturnAllProducts()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();

            //Act
            var r = await prodService.GetProductsAsync();

            //Assert
            Assert.NotEmpty(r);
        }

        [Fact]
        public async Task GetProductsDto_Valid_ReturnAllProductsDto()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();

            //Act
            var r = await prodService.GetProductsDtoAsync();

            //Assert
            Assert.NotEmpty(r);
        }

        [Fact]
        public void CalculateNormalPrice_NormaPrice_ReturnNormalPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 0;
            int quantidy = 5;

            //Act
            NormalCartProductService s = new NormalCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 50m);
            Assert.Equal(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 0);
        }

        [Fact]
        public void CalculateBuy1Get1FreePrice_Quanitdy1_ReturnNormalPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 1;
            int quantidy = 1;

            //Act
            CartProductService s = new NormalCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 10m);
            Assert.Equal(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 0m);
        }

        [Fact]
        public void CalculateBuy1Get1FreePrice_Quanitdy2_ReturnPromotionPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 1;
            int quantidy = 2;

            //Act
            CartProductService s = new Buy1Get1FreeCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 10m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 10m);
        }

        [Fact]
        public void CalculateBuy1Get1FreePrice_Quanitdy3_ReturnPromotionAppliedOncePrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 1;
            int quantidy = 3;

            //Act
            CartProductService s = new Buy1Get1FreeCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 20m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 10m);
        }

        [Fact]
        public void CalculateBuy1Get1FreePrice_Quanitdy4_ReturnPromotionAppliedTwicePrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 1;
            int quantidy = 4;

            //Act
            CartProductService s = new Buy1Get1FreeCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 20m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 20m);
        }

        [Fact]
        public void CalculateBuy1Get1FreePrice_Quanitdy5_ReturnPromotionAppliedTwicePrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 1;
            int quantidy = 5;

            //Act
            CartProductService s = new Buy1Get1FreeCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 30m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 20m);
        }

        [Fact]
        public void CalculateBuy1Get1FreePrice_Quanitdy6_ReturnPromotionAppliedThreeTimesPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 1;
            int quantidy = 6;

            //Act
            CartProductService s = new Buy1Get1FreeCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 30m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 30m);
        }

        [Fact]
        public void CalculateBuy1Get1FreePrice_Quanitdy41_ReturnPromotionAppliedTwentyTimesPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 1;
            int quantidy = 41;

            //Act
            CartProductService s = new Buy1Get1FreeCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 210m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 200m);
        }

        [Fact]
        public void CalculateBuy1Get1FreePrice_Quanitdy41_ReturnPromotionAppliedTwentyOneTimesPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 1;
            int quantidy = 42;

            //Act
            CartProductService s = new Buy1Get1FreeCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 210m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 210m);
        }

        [Fact]
        public void CalculateThreeforTenPrice_Quanitdy1_ReturnNormalPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 50m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 2;
            int quantidy = 1;

            //Act
            CartProductService s = new ThreeforTenCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 50m);
            Assert.Equal(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 0m);
        }

        [Fact]
        public void CalculateThreeforTenPrice_Quanitdy2_ReturnNormalPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 50m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 2;
            int quantidy = 2;

            //Act
            CartProductService s = new ThreeforTenCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 100m);
            Assert.Equal(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 0m);
        }

        [Fact]
        public void CalculateThreeforTenPrice_Quanitdy3_ReturnOnePromotionPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 2;
            int quantidy = 3;

            //Act
            CartProductService s = new ThreeforTenCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 10m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 20m);
        }

        [Fact]
        public void CalculateThreeforTenPrice_Quanitdy4_ReturnOnePromotionPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 2;
            int quantidy = 4;

            //Act
            CartProductService s = new ThreeforTenCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 20m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 20m);
        }

        [Fact]
        public void CalculateThreeforTenPrice_Quanitdy5_ReturnOnePromotionPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 2;
            int quantidy = 5;

            //Act
            CartProductService s = new ThreeforTenCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 30m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 20m);
        }

        [Fact]
        public void CalculateThreeforTenPrice_Quanitdy5_ReturnTwicePromotionPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 2;
            int quantidy = 6;

            //Act
            CartProductService s = new ThreeforTenCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 20m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 40m);
        }

        [Fact]
        public void CalculateThreeforTenPrice_Quanitdy7_ReturnTwicePromotionPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 2;
            int quantidy = 7;

            //Act
            CartProductService s = new ThreeforTenCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 30m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 40m);
        }

        [Fact]
        public void CalculateThreeforTenPrice_Quanitdy25_ReturnEightPromotionPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 2;
            int quantidy = 25;

            //Act
            CartProductService s = new ThreeforTenCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 90m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 160m);
        }

        [Fact]
        public void CalculateThreeforTenPrice_Quanitdy26_ReturnEightPromotionPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 2;
            int quantidy = 26;

            //Act
            CartProductService s = new ThreeforTenCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 100m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 160m);
        }

        [Fact]
        public void CalculateThreeforTenPrice_Quanitdy26_ReturnNinePromotionPrice()
        {
            //Arrange
            var prodService = ServiceProvider.GetService<IProductService>();
            Product prod = new Product();
            prod.Id = 1;
            prod.Price = 10m;
            prod.Promotion = new Promotion();
            prod.Promotion.TypeId = 2;
            int quantidy = 27;

            //Act
            CartProductService s = new ThreeforTenCartProductService(prod, quantidy);

            //Assert
            Assert.Equal(s.CartProduct.TotalPrice, 90m);
            Assert.NotEqual(s.CartProduct.PromotionApplied, string.Empty);
            Assert.Equal(s.CartProduct.Saved, 180m);
        }

        [Fact]
        public async Task GetShoppingCart_SomeProducts_ReturnShoppingCartWithData()
        {
            //Arrange
            var cartService = ServiceProvider.GetService<IShoppingCartService>();

            RequestCartProductDto cart = new RequestCartProductDto();
            cart.ProductId = 1;
            cart.Quantidy = 3;

            RequestCartProductDto cart2 = new RequestCartProductDto();
            cart2.ProductId = 2;
            cart2.Quantidy = 6;

            RequestCartProductDto cart3 = new RequestCartProductDto();
            cart3.ProductId = 3;
            cart3.Quantidy = 9;

            IEnumerable<RequestCartProductDto> prods = new List<RequestCartProductDto>() { cart, cart2, cart3 };

            //Act
            var c = await cartService.GetShoppingCart(prods);

            //Assert
            Assert.Equal(c.OriginalPrice, 102);
            Assert.Equal(c.Quantidy, 18);
            Assert.Equal(c.TotalPrice, 78m);
            Assert.Equal(c.Saved, 24m);
            Assert.Equal(c.CartProductDtos.Count(), 3);
        }
    }
}
