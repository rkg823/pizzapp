using System;
using System.Collections.Generic;

namespace PizzaAppService.Models
{
  public class Product
  {
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IList<Size> Sizes { get; set; }
    public IList<Product> Toppings { get; set; }
    public IList<Product> Sauces { get; set; }
  }
}
