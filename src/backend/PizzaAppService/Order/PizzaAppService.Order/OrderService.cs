using Microsoft.Extensions.Configuration;
using PizzaAppService.Models;
using PizzaAppService.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaAppService.Order
{
  public class OrderService
  {
    private readonly IConfiguration configuration;
    private readonly IProductService productService;
    public OrderService(IConfiguration configuration, IProductService productService)
    {
      this.configuration = configuration;
    }

    public async Task<Models.Order> CreateOrder(Models.Order order)
    {
      var path = configuration[""];

      foreach(var item in order.Items)
      {
        var product = await productService.Get(order.Items[0].Id);
        item.Product = product;
      }
      return order;
    }
  }
}
