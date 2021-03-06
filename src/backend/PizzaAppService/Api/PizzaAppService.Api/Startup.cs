using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using PizzaAppService.Common;
using PizzaAppService.Order;
using PizzaAppService.Product;
using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace PizzaAppService.Api
{
  public class Startup
  {
    private const string ALLOWED_ORIGINS = "AllowOrigins";
    private const string USER_AGENT = "PizzaApp";
    private const string ACCEPT = "application/vnd.github.v3+json";
    private const string BASE_API_ADDRESS_KEY = "GithubBaseAddress";
    private const string CLIENT_APP_ORIGIN_KEY = "ClientAppOrigin";
    private const string MEDIA_DIRECTORY = "AppData\\Medias";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(c =>
      {
        c.AddPolicy(ALLOWED_ORIGINS, options => {

          options
          .WithOrigins(Configuration[CLIENT_APP_ORIGIN_KEY])
          .AllowAnyHeader()
          .AllowAnyMethod();
        });
        
      });
      services.AddControllers();
      services.AddHttpClient<IGithubService, GithubService>(c =>
      {
        
        c.BaseAddress = new Uri(Configuration[BASE_API_ADDRESS_KEY]);
        c.DefaultRequestHeaders.Add("Accept", ACCEPT);
        c.DefaultRequestHeaders.Add("User-Agent", USER_AGENT);
      });
      services.AddSingleton<IProductService, ProductService>();
      services.AddSingleton<IProductMapperService, ProductMapperService>();
      services.AddSingleton<IOrderService, OrderService>();
      services.AddSingleton<IFileSystem>((_) => new FileSystem());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/error");
      }

      app.UseHttpsRedirection();

      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(env.ContentRootPath, MEDIA_DIRECTORY)),
        RequestPath = "/media",
        OnPrepareResponse = ctx =>
        {
          ctx.Context.Response.Headers.Append(
               "Cache-Control", $"public, max-age={Configuration["MediaCacheControl"]}");
        }
      });

      app.UseRouting();

      app.UseCors(ALLOWED_ORIGINS);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
