using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PizzaAppService.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace PizzaAppService.Product
{
  public class ProductService : IProductService
  {
    private const string PRODUCTS_FILE_URL = "data/products.json";

    private readonly IGithubService gitHubService;

    public ProductService(IGithubService gitHubService)
    {
      this.gitHubService = gitHubService;
    }
    public async Task<IList<Models.Product>> Get()
    {
      try
      {
        var json = await gitHubService.GetFile(PRODUCTS_FILE_URL);
        dynamic data = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());
      }catch(Exception ex)
      {

      }
      return new List<Models.Product>();
    }

  }
}
