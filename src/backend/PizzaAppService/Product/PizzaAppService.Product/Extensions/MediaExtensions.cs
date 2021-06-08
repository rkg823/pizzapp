using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaAppService.Product.Extensions
{
  public static class MediaExtensions
  {
    public static IList<Models.Media> ToMediaList(this IList<dynamic> list)
    {
     
      return list.Select((img, index) => ToMedia(img as string, index))
          .Where(media => media != null)
          .ToList();
    }

    public static Models.Media ToMedia(this string url, int index)
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
  }
}
