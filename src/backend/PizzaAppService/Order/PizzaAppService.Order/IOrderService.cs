using System.Threading.Tasks;

namespace PizzaAppService.Order
{
  public interface IOrderService
  {
    Task<Models.Order> CreateOrderAsync(Models.Order order);
    Task<Models.Order> ConfirmOrderAsync(string id);
  }
}