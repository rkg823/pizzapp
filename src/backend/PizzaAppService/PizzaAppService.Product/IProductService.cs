using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaAppService.Product
{
  public interface IProductService
  {
    Task<IList<Models.Product>> Get();
  }
}