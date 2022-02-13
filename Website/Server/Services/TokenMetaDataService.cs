using CryptoGuitars.Shared.DTOs;

namespace CryptoGuitars.Server.Services;

public class TokenMetaDataService : ITokenMetaDataService
{
    private readonly HttpClient _httpClient;

    public TokenMetaDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TokenMetaDataDTO?> GetAsync(Uri tokenUri)
    {
        var response = await _httpClient.GetAsync(tokenUri);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TokenMetaDataDTO>();
    }
}