using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PizzaAppService.Common;

namespace PizzaAppService.Product.Test
{
  [TestClass]
  public class ProductServiceTest
  {
    private Mock<IGithubService> mockGithubService;

    private Mock<ILogger<ProductService>> mockLogger;

    private Mock<IProductMapperService> mockProductMapperService;

    [TestInitialize]
    public void Init()
    {
      mockGithubService = new Mock<IGithubService>();
      mockLogger = new Mock<ILogger<ProductService>>();
      mockProductMapperService = new Mock<IProductMapperService>();
    }

    [TestMethod]
    public void GetShouldReturnProducts()
    {
    }
  }
}
