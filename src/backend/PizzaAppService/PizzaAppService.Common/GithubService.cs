using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PizzaAppService.Common
{
  public class GithubService : IGithubService
  {

    public readonly HttpClient client;

    public GithubService(HttpClient client)
    {
      this.client = client;
    }

    public async Task<string> GetFile(string url)
    {
      var response = await client.GetAsync(url);
      response.EnsureSuccessStatusCode();
      return await response.Content.ReadAsStringAsync();
    }
  }
}
