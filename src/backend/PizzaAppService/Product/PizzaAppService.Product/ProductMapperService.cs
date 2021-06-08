using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaAppService.Product
{
  public class ProductMapperService : IProductMapperService
  {
    private const string NOT_APPLICABLE = "N/A";

    private readonly ILogger<ProductMapperService> logger;

    public ProductMapperService(ILogger<ProductMapperService> logger)
    {
      this.logger = logger;
    }

    public Models.Media ToMedia(string url, int index)
    {
      if (string.IsNullOrWhiteSpace(url))
      {
        return null;
      }

      return new Models.Media
      {
        Id = Guid.NewGuid().ToString(),
        Url = url,
        IsPrimary = index == 0
      };
    }

    public Models.Product ToChildProduct(dynamic details)
    {

      if (!double.TryParse(details.price.amount.ToString(), out double amount))
      {
        logger.LogWarning("Invalid amount.", new[] { details });
        return null;
      }

      if (string.IsNullOrWhiteSpace(details.title))
      {
        logger.LogWarning("Invalid title.", new[] { details });
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

    public Models.Size ToSize(dynamic sizeDetails)
    {

      if (string.IsNullOrWhiteSpace(sizeDetails.title))
      {
        logger.LogWarning("Invalid title.", new[] { sizeDetails });
        return null;
      }

      var size = new Models.Size
      {
        Id = sizeDetails.id,
        Title = sizeDetails.title,
        Description = sizeDetails.description
      };
      return size;
    }
  }
}
