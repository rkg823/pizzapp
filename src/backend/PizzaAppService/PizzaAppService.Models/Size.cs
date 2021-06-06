using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaAppService.Models
{
  public class Size
  {
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Price Price { get; set; }
  }
}
