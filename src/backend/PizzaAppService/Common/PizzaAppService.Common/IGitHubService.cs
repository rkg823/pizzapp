using System.Net.Http;
using System.Threading.Tasks;

namespace PizzaAppService.Common
{
  public interface IGithubService
  {
    Task<string> GetFile(string url);
  }
}