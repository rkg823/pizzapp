using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaAppService.Order;

namespace PizzaAppService.Api.Controllers
{
  [Route("api/order")]
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly IOrderService orderService;
    public OrderController(IOrderService orderService)
    {
      this.orderService = orderService;
    }

    [HttpPost]
    public async Task<Models.Order> Post([FromBody] Models.Order order)
    {
      return await orderService.CreateOrderAsync(order);
    }

    [HttpPut]
    public async Task<Models.Order> Put([FromBody] Models.Order order)
    {
      return await orderService.ConfirmOrderAsync(order.Id);
    }
  }
}
