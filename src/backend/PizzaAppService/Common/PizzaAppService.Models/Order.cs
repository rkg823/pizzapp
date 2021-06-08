using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzaAppService.Models
{
  public class Order: Base
  {
    [Required]
    public string ProductId { get; set; }
    [Required]
    public IList<OrderItem> Items { get; set; }
    [Required]
    public double Amount { get; set; }
    public OrderStatus Status { get; set; }
  }
}
