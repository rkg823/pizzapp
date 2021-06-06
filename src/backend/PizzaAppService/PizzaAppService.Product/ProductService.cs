using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PizzaAppService.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
    private const string NOT_APPLICABLE = "N/A";
    private readonly ILogger<ProductService> logger;

    private readonly IGithubService gitHubService;

    public ProductService(IGithubService gitHubService, ILogger<ProductService> logger)
    {
      this.gitHubService = gitHubService;
      this.logger = logger;
    }
    public async Task<IList<Models.Product>> Get()
    {

      var results = await Task.WhenAll(new[]
      {
        GetFileData(PRODUCTS_FILE_URL),
        GetFileData(SIZES_FILE_URL),
        GetFileData(SAUCES_FILE_URL), 
        GetFileData(TOPPINGS_FILE_URL)
      });

      var products = results[0];
      var sizes = results[1]
        .ToDictionary(size => size.id as string, size => size);
      var sauces = results[2]
        .ToDictionary(sauce => sauce.id as string, sauce => sauce);
      var toppings = results[3]
        .ToDictionary(topping => topping.id as string, topping => topping);
     

      var dto = products.Select(product => new Models.Product
      {
        Id = product.id,
        Title = product.title,
        Description = product.description,
        Sizes = (product.details.availableSizes as IList<dynamic>)
        .Where(size => sizes.ContainsKey(size.id as string))
          .Select(size => ToSize(size, sizes) as Models.Size)
          .Where(size => size != null)
          .ToList(),
        Toppings = (product.details.availableToppings as IList<dynamic>)
         .Where(topping => toppings.ContainsKey(topping.id as string))
         .Select(topping => ToChildProduct(topping, toppings) as Models.Product)
         .Where(topping => topping != null)
         .ToList(),
        Sauces = (product.details.availableSauces as IList<dynamic>)
         .Where(sauce => toppings.ContainsKey(sauce.id as string))
         .Select(sauce => ToChildProduct(sauce, sauces) as Models.Product)
         .Where(sauce => sauce != null)
         .ToList()
      }).ToList();
      return dto;
    }

    private Models.Product ToChildProduct(dynamic item, Dictionary<string, dynamic> source)
    {
      var details = source[item.id as string];

      if (!double.TryParse(details.price.amount, out double amount))
      {
        logger.LogWarning("Invalid amount.", new[] { item, details });
        return null;
      }

      if (string.IsNullOrWhiteSpace(details.title))
      {
        logger.LogWarning("Invalid title.", new[] { item, details });
        return null;
      }

      var product = new Models.Product
      {
        Id = details.id,
        Title = details.title,
        Description = details.description,
        Sizes = new List<Models.Size>
             {
               new Models.Size
               {
                 Id = NOT_APPLICABLE,
                 Description = NOT_APPLICABLE,
                 Title = NOT_APPLICABLE,
                 Price = new Models.Price
                 {
                   Amount = amount
                 }

               }
             }
      };
      return product;
    }

    private Models.Size ToSize(dynamic item, Dictionary<string, dynamic> source)
    {
      var sizeDetails = source[item.id as string];
      if (!double.TryParse(item.ammount, out double amount))
      {
        logger.LogWarning("Invalid amount.", new[] { item, sizeDetails });
        return null;
      }

      if (string.IsNullOrWhiteSpace(sizeDetails.title))
      {
        logger.LogWarning("Invalid title.", new[] { item, sizeDetails });
        return null;
      }

      var size = new Models.Size
      {
        Id = sizeDetails.id,
        Title = sizeDetails.title,
        Description = sizeDetails.description,
        Price = new Models.Price
        {
          Id = Guid.NewGuid().ToString(),
          Amount = amount
        }
      };
      return size;
    }

    private async Task<IList<dynamic>> GetFileData(string url)
    {
      var file = await gitHubService.GetFile(url);
      dynamic json = JsonConvert.DeserializeObject<ExpandoObject>(file, new ExpandoObjectConverter());
      return json.data as IList<dynamic>;
    }
  }
}
