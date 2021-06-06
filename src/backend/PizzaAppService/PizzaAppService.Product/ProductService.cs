using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using Models = PizzaAppService.Models;

namespace PizzaAppService.Product
{
  public class ProductService
  {
    private readonly IFileSystem fileSystem;
    public ProductService(IFileSystem fileSystem)
    {
      this.fileSystem = fileSystem;
    }
    public IList<Models.Product> GetProducts()
    {
      fileSystem.File.ReadAllText("");
    }

  }
}
