using CryptoGuitars.Shared.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoGuitars.Server.Services;

public class TokenMetaDataService : ITokenMetaDataService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;

    public TokenMetaDataService(
        HttpClient httpClient,
        IMemoryCache memoryCache)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
    }

    public async Task<TokenMetaDataDTO?> GetAsync(Uri tokenUri)
    {
        return await _memoryCache.GetOrCreateAsync(tokenUri, async cacheEntry =>
        {
            var response = await _httpClient.GetAsync(tokenUri);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TokenMetaDataDTO>();
        });
    }
}