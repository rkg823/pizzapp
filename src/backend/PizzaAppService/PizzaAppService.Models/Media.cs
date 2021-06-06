using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaAppService.Models
{
  public class Media
  {
    public string Id { get; set; }
    public string Description { get; set; }
    public Uri Url { get; set; }
    public MediaType Type { get; set; }

  }
}
