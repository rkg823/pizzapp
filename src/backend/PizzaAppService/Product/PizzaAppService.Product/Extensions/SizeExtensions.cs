using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaAppService.Product.Extensions
{
  public static class SizeExtensions
  {
    public static IList<Models.Size> ToSizeList(this IList<dynamic> items, IDictionary<string, Models.Size> source)
    {
      return items.Where(item => source.ContainsKey(item.id as string))
           .Select(item => {
             if (!double.TryParse(item.price.amount.ToString(), out double amount)) {
               throw new InvalidCastException($"Invalid price for size id: {item.id}");
             }
             var size = source[item.id as string];
             size.Price = amount;
             return size;
           })
           .ToList();
    }
  }
}
