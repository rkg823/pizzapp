using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PizzaAppService.Models;
using PizzaAppService.Product;
using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaAppService.Order
{
  public class OrderService : IOrderService
  {
    private const string CONTENT_ROOT_KEY = "contentRoot";
    private const string ORDER_DIRECTORY = "AppData\\Orders";

    private readonly IConfiguration configuration;

    private readonly IProductService productService;

    private readonly IFileSystem fileSystem;

    public OrderService(IConfiguration configuration, IProductService productService, IFileSystem fileSystem)
    {
      this.configuration = configuration;
      this.productService = productService;
      this.fileSystem = fileSystem;
    }

    public async Task<Models.Order> CreateOrderAsync(Models.Order order)
    {
      foreach (var item in order.Items)
      {
        item.Price = await productService.GetPriceById(item.Id, order.ProductId);
      }
      order.Amount = order.Items.Sum(item => item.Price);
      order.Id = Guid.NewGuid().ToString();
      order.Status = OrderStatus.Initiated;
      var path = Path.Combine(configuration[CONTENT_ROOT_KEY], ORDER_DIRECTORY, $"{order.Id}.json");
      fileSystem.File.WriteAllText(path, JsonConvert.SerializeObject(order));

      return order;
    }

    public async Task<Models.Order> ConfirmOrderAsync(string id)
    {
      var path = Path.Combine(configuration[CONTENT_ROOT_KEY], ORDER_DIRECTORY, $"{id}.json");
      var fileText = await fileSystem.File.ReadAllTextAsync(path);
      var order = JsonConvert.DeserializeObject<Models.Order>(fileText);
      order.Status = OrderStatus.Confirmed;
      fileSystem.File.WriteAllText(path, JsonConvert.SerializeObject(order));
      return order;
    }

  }
}
