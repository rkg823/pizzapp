using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PizzaAppService.Models
{
  public class Order: Base
  {
    public IList<OrderItem> Items { get; set; }
    public double Ammout { get; set; }
  }
}
