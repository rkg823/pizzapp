using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using PizzaAppService.Models;
using PizzaAppService.Product;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace PizzaAppService.Order.Test
{
  [TestClass]
  public class OrderServiceTest
  {
    private Mock<IConfiguration> mockConfiguration;
    private Mock<IProductService> mockProductService;
    private Mock<IFileSystem> mockFileSystem;

    [TestInitialize]
    public void Init()
    {
      mockConfiguration = new Mock<IConfiguration>();
      mockProductService = new Mock<IProductService>();
      mockFileSystem = new Mock<IFileSystem>();
    }

    [TestMethod]
    public async Task CreateOrderAsyncShouldReturnCreatedOrder()
    {
      mockProductService.Setup(ps => ps.GetPriceById(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(100);
      mockConfiguration.SetupGet(config => config["contentRoot"]).Returns("root");
      mockFileSystem.Setup(fs => fs.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

      var orderService = new OrderService(mockConfiguration.Object, mockProductService.Object, mockFileSystem.Object);
      var order = await orderService.CreateOrderAsync(mockOrder(200, 200));

      mockProductService.Verify(ps => ps.GetPriceById("test-item-id", "test-product-id"), Times.Once());
      var expectedOrderToWrite = mockOrder(100.0, 100.0);
      expectedOrderToWrite.Id = order.Id;
      expectedOrderToWrite.Status = OrderStatus.Initiated;

      mockFileSystem.Verify(fs => fs.File.WriteAllText($"root\\AppData\\Orders\\{order.Id}.json", 
        JsonConvert.SerializeObject(expectedOrderToWrite)), Times.Once());
    }

    [TestMethod]
    public async Task CreateOrderAsyncShouldThrowExceptionForFailureInGetPriceById()
    {
      mockProductService.Setup(ps => ps.GetPriceById(It.IsAny<string>(), It.IsAny<string>())).Throws(new TimeoutException());
      mockConfiguration.SetupGet(config => config["contentRoot"]).Returns("root");
      mockFileSystem.Setup(fs => fs.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

      var orderService = new OrderService(mockConfiguration.Object, mockProductService.Object, mockFileSystem.Object);
      await Assert.ThrowsExceptionAsync<TimeoutException>(async () => await orderService.CreateOrderAsync(mockOrder(200, 200)));
      mockFileSystem.Verify(fs => fs.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
      mockProductService.Verify(ps => ps.GetPriceById(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
    }

    private Models.Order mockOrder(double amount, double price)
    {
      return new Models.Order
      {
        ProductId = "test-product-id",
        Amount = amount,
        Items = new List<Models.OrderItem>
        {
          new Models.OrderItem
          {
            Id = "test-item-id",
            Price = price,
            Title ="test-item"
          }
        }
      };
    }
  }
}
