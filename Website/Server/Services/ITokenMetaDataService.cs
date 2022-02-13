using CryptoGuitars.Shared.DTOs;

namespace CryptoGuitars.Server.Services;

public interface ITokenMetaDataService
{
    Task<TokenMetaDataDTO?> GetAsync(Uri tokenUri);
}
