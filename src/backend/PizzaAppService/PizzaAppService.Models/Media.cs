using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaAppService.Models
{
  public class Media: Base
  {
    public string Url { get; set; }
    public MediaType Type { get; set; }
    public bool IsPrimary { get; set; }
  }
}
