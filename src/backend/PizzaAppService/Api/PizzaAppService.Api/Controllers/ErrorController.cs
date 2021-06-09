using Microsoft.AspNetCore.Mvc;

namespace PizzaAppService.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ErrorController : ControllerBase
  {
    [Route("/error")]
    public IActionResult Error() => Problem();
  }
}
