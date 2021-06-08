using PizzaAppService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaAppService.Product.Extensions
{
  public static class BaseExtensions
  {
    public static IDictionary<string, T> ToDictionary<T>(this IList<T> products) where T: Base
    {
      return products.ToDictionary(product => product.Id, product => product);
    }

    public static IList<T> ToList<T>(this IList<dynamic> items, IDictionary<string, T> source) where T : Base
    {
      return items.Where(id => source.ContainsKey(id as string))
            .Select(id => source[id as string])
            .ToList();
    }
  }
}
