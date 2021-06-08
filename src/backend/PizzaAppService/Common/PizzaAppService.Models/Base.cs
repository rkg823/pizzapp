using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzaAppService.Models
{
  public class Base
  {
    public string Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
  }
}
