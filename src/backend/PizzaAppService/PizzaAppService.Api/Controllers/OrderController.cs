using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaAppService.Order;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
    // POST api/<OrderController>
    [HttpPost]
    public async Task<Models.Order> Post([FromBody] Models.Order order)
    {
      return await orderService.CreateOrder(order);
    }

    [HttpPut]
    public async Task<Models.Order> Put([FromBody] Models.Order order)
    {
      return await orderService.ConfirmOrder(order.Id);
    }
  }
}
