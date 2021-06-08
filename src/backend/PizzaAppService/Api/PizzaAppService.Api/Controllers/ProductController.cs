using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaAppService.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaAppService.Api.Controllers
{
  [ApiController]
  [Route("api/product")]
  public class ProductController : ControllerBase
  {
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
      this.productService = productService;
    }

    [HttpGet]
    public async Task<IList<Models.Product>> Get()
    {
      return await productService.Get();
    }
  }
}
