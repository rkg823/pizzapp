using PizzaAppService.Models;

namespace PizzaAppService.Product
{
  public interface IProductMapperService
  {
    Models.Product ToChildProduct(dynamic details);
    Media ToMedia(string url, int index);
    Size ToSize(dynamic sizeDetails);
  }
}