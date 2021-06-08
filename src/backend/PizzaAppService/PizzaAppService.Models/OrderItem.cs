using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaAppService.Models
{
  public class OrderItem: Base
  {

    public IList<OrderItem> AssociatedItems { get; set; }

    public Product Product { get; set; }
  }
}
