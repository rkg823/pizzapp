using System.Threading.Tasks;

namespace PizzaAppService.Order
{
  public interface IOrderService
  {
    Task<Models.Order> CreateOrder(Models.Order order);
    Task<Models.Order> ConfirmOrder(string id);
  }
}