using PizzaAppService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaAppService.Product
{
  public interface IProductService
  {
    Task<IList<Models.Product>> GetAsync();
    Task<Models.Product> GetAsnc(string id);
    Task<double> GetPriceById(string id, string productId);
  }
}