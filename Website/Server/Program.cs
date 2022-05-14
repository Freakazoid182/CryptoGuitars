using Nethereum.Web3;
using CryptoGuitars.Server.Services;
using CryptoGuitars.Contracts.CryptoGuitarsNFT;

using Microsoft.Net.Http.Headers;
using CryptoGuitars.Contracts.CryptoGuitarsMarketPlace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCaching();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Web3>(services =>
    new Web3(builder.Configuration.GetValue<string>("Web3:BaseUrl")));

builder.Services.AddScoped<CryptoGuitarsNFTService>(services =>
    new CryptoGuitarsNFTService(services.GetRequiredService<Web3>(),
    builder.Configuration.GetValue<string>("Contracts:CryptoGuitarsNFT:Address")));

    builder.Services.AddScoped<CryptoGuitarsMarketPlaceService>(services =>
    new CryptoGuitarsMarketPlaceService(services.GetRequiredService<Web3>(),
    builder.Configuration.GetValue<string>("Contracts:CryptoGuitarsMarketPlace:Address")));

var httpClientRegistration = builder.Services.AddHttpClient<ITokenMetaDataService, TokenMetaDataService>(options =>
{
    options.BaseAddress = new Uri("https://ipfs.io/ipfs/QmXgYAq8vPzjACYpJVgNdUxpMpXN6HfAV7zUDrMqbtofos/");
    options.Timeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy
    .WithOrigins(app.Configuration.GetSection("CORS:AllowedOrigins").Get<string[]>())
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType));

app.UseResponseCaching();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
