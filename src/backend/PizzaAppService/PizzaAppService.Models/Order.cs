using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PizzaAppService.Models
{
  public class Order: Base
  {
    public string ProductId { get; set; }
    public IList<OrderItem> Items { get; set; }
    public double Amount { get; set; }
    public OrderStatus Status { get; set; }
  }
}
