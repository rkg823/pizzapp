using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PizzaAppService.Api.Controllers;
using PizzaAppService.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaAppService.Api.Test
{
  [TestClass]
  public class ProductControllerTest
  {
    private Mock<IProductService> mockProductService;

    [TestInitialize]
    public void Init()
    {
      mockProductService = new Mock<IProductService>();
    }

    [TestMethod]
    public async Task GetShoudReturnProducts()
    {
      var testProducts = new List<Models.Product>
      {
        new Models.Product
        {
          Id = "test-product-id",
          Cheeses = new List<Models.Product>
          {
            new Models.Product
            {
               Id = "test-cheese-id",
            }
          },
          Sauces = new List<Models.Product>
          {
            new Models.Product
            {
                 Id = "test-sauce-id",
            }
          },
          Toppings = new List<Models.Product>
          {
            new Models.Product
            {
                 Id = "test-topping-id",
            }
          },
        }
      };
      mockProductService.Setup(ps => ps.GetAsync()).ReturnsAsync(testProducts);

      var controller = new ProductController(mockProductService.Object);
      var products = await controller.Get();

      Assert.AreEqual(1, products.Count);
      Assert.AreEqual("test-product-id", products.First().Id);
      Assert.AreEqual("test-cheese-id", products.First().Cheeses.First().Id);
      Assert.AreEqual("test-sauce-id", products.First().Sauces.First().Id);
      Assert.AreEqual("test-topping-id", products.First().Toppings.First().Id);
    }

    [TestMethod]
    public async Task GetShouldThrowError()
    {
      mockProductService.Setup(ps => ps.GetAsync()).Throws(new TimeoutException());

      var controller = new ProductController(mockProductService.Object);
      await Assert.ThrowsExceptionAsync<TimeoutException>(async () => await controller.Get());
    }
  }
}
