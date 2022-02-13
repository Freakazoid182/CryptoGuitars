using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CryptoGuitars.Client;
using MudBlazor.Services;
using MetaMask.Blazor;
using Nethereum.Web3;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IWeb3>(services => new Web3(builder.Configuration.GetValue<string>("Web3:BaseUrl")));
builder.Services.AddMetaMaskBlazor();
builder.Services.AddHttpClient("default", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient("server", client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Server:BaseUrl")));
builder.Services.AddMudServices();

await builder.Build().RunAsync();
