using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TiendaProducto_Cliente.Services;
using TiendaProducto_Cliente.Services.IService;

namespace TiendaProducto_Cliente
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseApiUrl")) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IBookOrderDetailService, BookOrderDetailService>();
            builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            await builder.Build().RunAsync();
        }
    }
}
