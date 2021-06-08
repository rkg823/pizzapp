using System;
using System.Collections.Generic;

namespace PizzaAppService.Models
{
  public class Product: Base
  {
    public IList<Size> Sizes { get; set; }
    public IList<Product> Toppings { get; set; }
    public IList<Product> Sauces { get; set; }
    public IList<Product> Cheeses { get; set; }
    public IList<Media> Medias { get; set; }
  }
}
