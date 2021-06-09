using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaAppService.Common.Test
{
  [TestClass]
  public class GithubServiceTest
  {
    private Mock<HttpMessageHandler> mockHttpHandler;

    [TestInitialize]
    public void Init()
    {
      mockHttpHandler = new Mock<HttpMessageHandler>();
    }

    [TestMethod]
    public async Task GetFileShouldReturnFileText()
    {
      var text = "{\"value\": \"test\"}";

      mockHttpHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
      .ReturnsAsync(new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(text) });

      var httpClient = new HttpClient(mockHttpHandler.Object);

      var githubService = new GithubService(httpClient);
      var content = await githubService.GetFile("http://localhost:/testfile.json");

      Assert.AreEqual("{\"value\": \"test\"}", content);
      mockHttpHandler.Protected().Verify<Task<HttpResponseMessage>>("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
    }

    [TestMethod]
    public async Task GetFileShouldThrowExceptionForUnsuccessfulResponse()
    {
      var text = "{\"value\": \"test\"}";

      mockHttpHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
      .ReturnsAsync(new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.InternalServerError, Content = new StringContent(text) });

      var httpClient = new HttpClient(mockHttpHandler.Object);

      var githubService = new GithubService(httpClient);
      await Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await githubService.GetFile("http://localhost:/testfile.json"));
      mockHttpHandler.Protected().Verify<Task<HttpResponseMessage>>("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
    }

    [TestMethod]
    public async Task GetFileShouldThrowExceptionForInvalidUrl()
    {
      var text = "{\"value\": \"test\"}";

      mockHttpHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
      .ReturnsAsync(new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.InternalServerError, Content = new StringContent(text) });

      var httpClient = new HttpClient(mockHttpHandler.Object);

      var githubService = new GithubService(httpClient);
      await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await githubService.GetFile("testfile.json"));

      mockHttpHandler.Protected().Verify<Task<HttpResponseMessage>>("SendAsync", Times.Never(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
    }
  }
}
