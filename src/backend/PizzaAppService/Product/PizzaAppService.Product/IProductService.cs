using PizzaAppService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaAppService.Product
{
  public interface IProductService
  {
    Task<IList<Models.Product>> Get();
    Task<Models.Product> Get(string id);
    Task<Models.Product> GetCheeseById(string id);
    Task<IList<Models.Product>> GetCheeses();
    Task<Models.Product> GetSauceById(string id);
    Task<IList<Models.Product>> GetSauces();
    Task<Size> GetSizeById(string id);
    Task<IList<Size>> GetSizes();
    Task<Models.Product> GetToppingById(string id);
    Task<IList<Models.Product>> GetToppings();
    Task<double> GetPriceById(string id, string productId);
  }
}