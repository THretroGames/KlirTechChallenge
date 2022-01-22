using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Klir.TechChallenge.Web.Api.DTOs;
using Klir.TechChallenge.Web.Api.Entities;

namespace Klir.TechChallenge.Web.Api.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetShoppingCart(IEnumerable<RequestCartProductDto> cartProdutcs);        
    }
}