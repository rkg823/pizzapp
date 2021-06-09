using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PizzaAppService.Common;
using PizzaAppService.Product.Extensions;
using System.Collections.Generic;
using System.Dynamic;
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

    public async Task<IList<Models.Product>> GetAsync()
    {
      var results = await Task.WhenAll(new[]
      {
        GetSaucesAsync(),
        GetToppingsAsync(),
        GetCheesesAsync()
      });

      var products = await GetFileDataAsync(PRODUCTS_FILE_URL);
      var sizes = (await GetSizesAsync()).ToDictionary();
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

    public async Task<Models.Product> GetAsnc(string id)
    {
      var products = await GetAsync();
      return products.FirstOrDefault(product => product.Id == id);
    }

    public async Task<double> GetPriceById(string id, string productId)
    {
      var product = await GetAsnc(productId);

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

    private async Task<IList<Models.Product>> GetToppingsAsync()
    {
      var toppings = await GetFileDataAsync(TOPPINGS_FILE_URL);
      return toppings
        .Select(topping => this.productMapper.ToChildProduct(topping) as Models.Product)
        .ToList();
    }

    private async Task<IList<Models.Product>> GetSaucesAsync()
    {
      var data = await GetFileDataAsync(SAUCES_FILE_URL);
      return data
          .Select(sauce => this.productMapper.ToChildProduct(sauce) as Models.Product)
          .ToList();
    }

    private async Task<IList<Models.Size>> GetSizesAsync()
    {
      var sizes = await GetFileDataAsync(SIZES_FILE_URL);
      return sizes
           .Select(size => this.productMapper.ToSize(size) as Models.Size)
           .ToList();
    }

    private async Task<IList<Models.Product>> GetCheesesAsync()
    {
      var cheeses = await GetFileDataAsync(CHEESES_FILE_URL);
      return cheeses
          .Select(cheese => this.productMapper.ToChildProduct(cheese) as Models.Product)
          .ToList();
    }

    private async Task<IList<dynamic>> GetFileDataAsync(string url)
    {
      var file = await gitHubService.GetFile(url);
      dynamic json = JsonConvert.DeserializeObject<ExpandoObject>(file, new ExpandoObjectConverter());
      return json.data as IList<dynamic>;
    }
  }
}
