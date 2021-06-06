using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaAppService.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaAppService.Api.Controllers
{
  [ApiController]
  [Route("api/products")]
  public class ProductController : ControllerBase
  {

    private readonly ILogger<ProductController> logger;

    private readonly IProductService productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
      this.logger = logger;
      this.productService = productService;
    }

    [HttpGet]
    public async Task<IList<Models.Product>> Get()
    {
      return await productService.Get();
    }
  }
}
