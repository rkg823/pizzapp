using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PizzaAppService.Common;
using PizzaAppService.Product.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaAppService.Product
{
  public class ProductService : IProductService
  {
    private const string PRODUCTS_FILE_URL = "data/products.json";
    private const string SIZES_FILE_URL = "data/sizes.json";
    private const string SAUCES_FILE_URL = "data/sauces.json";
    private const string TOPPINGS_FILE_URL = "data/toppings.json";
    private const string CHEESES_FILE_URL = "data/cheeses.json";

    private readonly ILogger<ProductService> logger;

    private readonly IProductMapperService productMapper;

    private readonly IGithubService gitHubService;

    public ProductService(IGithubService gitHubService, ILogger<ProductService> logger,
      IProductMapperService productMapper)
    {
      this.gitHubService = gitHubService;
      this.logger = logger;
      this.productMapper = productMapper;
    }
    public async Task<IList<Models.Product>> Get()
    {
      var results = await Task.WhenAll(new[]
      {
        GetSauces(),
        GetToppings(),
        GetCheeses()
      });

      var products = await GetFileData(PRODUCTS_FILE_URL);
      var sizes = (await GetSizes()).ToDictionary();
      var sauces = results[0].ToDictionary();
      var toppings = results[1].ToDictionary();
      var cheeses = results[2].ToDictionary();

      return products.Select(product => new Models.Product
      {
        Id = product.id,
        Title = product.title,
        Medias = (product.details.imagesUrls as IList<dynamic>)
        .ToMediaList(),
        Description = product.description,
        Sizes = (product.details.availableSizes as IList<dynamic>)
          .ToSizeList(sizes),
        Toppings = (product.details.availableToppings as IList<dynamic>)
         .ToList(toppings),
        Sauces = (product.details.availableSauces as IList<dynamic>)
         .ToList(sauces),
        Cheeses = (product.details.availableCheeses as IList<dynamic>)
         .ToList(cheeses),
      }).ToList();
    }

    public async Task<Models.Product> Get(string id)
    {
      var products = await Get();
      return products.FirstOrDefault(product => product.Id == id);
    }

    public async Task<double> GetPriceById(string id, string productId)
    {
      var product = await Get(productId);

      var priceList = new Dictionary<string, double>();
      foreach (var size in product.Sizes)
      {
        priceList.Add(size.Id, size.Price);
      }
      foreach (var sauce in product.Sauces)
      {
        priceList.Add(sauce.Id, sauce.Sizes.First().Price);
      }
      foreach (var topping in product.Toppings)
      {
        priceList.Add(topping.Id, topping.Sizes.First().Price);
      }
      foreach (var cheese in product.Cheeses)
      {
        priceList.Add(cheese.Id, cheese.Sizes.First().Price);
      }

      return priceList[id];
    }

    public async Task<IList<Models.Product>> GetToppings()
    {
      var toppings = await GetFileData(TOPPINGS_FILE_URL);
      return toppings
        .Select(topping => this.productMapper.ToChildProduct(topping) as Models.Product)
        .ToList();
    }

    public async Task<Models.Product> GetToppingById(string id)
    {
      var toppings = await GetToppings();
      return toppings.FirstOrDefault(topping => topping.Id == id);
    }

    public async Task<IList<Models.Product>> GetSauces()
    {
      var data = await GetFileData(SAUCES_FILE_URL);
      return data
          .Select(sauce => this.productMapper.ToChildProduct(sauce) as Models.Product)
          .ToList();
    }

    public async Task<Models.Product> GetSauceById(string id)
    {
      var sauces = await GetSauces();
      return sauces.FirstOrDefault(sauce => sauce.Id == id);
    }

    public async Task<IList<Models.Size>> GetSizes()
    {
      var sizes = await GetFileData(SIZES_FILE_URL);
      return sizes
           .Select(size => this.productMapper.ToSize(size) as Models.Size)
           .ToList();
    }

    public async Task<Models.Size> GetSizeById(string id)
    {
      var sizes = await GetSizes();
      return sizes.FirstOrDefault(size => size.Id == id);
    }

    public async Task<IList<Models.Product>> GetCheeses()
    {
      var cheeses = await GetFileData(CHEESES_FILE_URL);
      return cheeses
          .Select(cheese => this.productMapper.ToChildProduct(cheese) as Models.Product)
          .ToList();
    }
    public async Task<Models.Product> GetCheeseById(string id)
    {
      var cheeses = await GetCheeses();
      return cheeses.FirstOrDefault(cheese => cheese.Id == id);
    }

    private async Task<IList<dynamic>> GetFileData(string url)
    {
      var file = await gitHubService.GetFile(url);
      dynamic json = JsonConvert.DeserializeObject<ExpandoObject>(file, new ExpandoObjectConverter());
      return json.data as IList<dynamic>;
    }
  }
}
