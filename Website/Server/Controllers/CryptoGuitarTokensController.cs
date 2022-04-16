using Microsoft.AspNetCore.Mvc;
using CryptoGuitars.Shared.DTOs;
using CryptoGuitars.Server.Services;
using CryptoGuitars.Contracts.CryptoGuitarsNFT;
using CryptoGuitars.Shared.Enums;
using CryptoGuitars.Shared.Extensions;
using System.ComponentModel.DataAnnotations;
using CryptoGuitars.Contracts.CryptoGuitarsMarketPlace;
using CryptoGuitars.Contracts.CryptoGuitarsMarketPlace.ContractDefinition;

namespace CryptoGuitars.Server.Controllers;

[ApiController]
[Route("api/v1/crypto-guitars-tokens")]
public class CryptoGuitarTokensController : ControllerBase
{
    private readonly ILogger<CryptoGuitarTokensController> _logger;
    private readonly CryptoGuitarsNFTService _nftService;
    private readonly CryptoGuitarsMarketPlaceService _marketPlaceService;
    private readonly ITokenMetaDataService _tokenMetaDataService;

    public CryptoGuitarTokensController(
        ILogger<CryptoGuitarTokensController> logger,
        CryptoGuitarsNFTService nftService,
        CryptoGuitarsMarketPlaceService marketPlaceService,
        ITokenMetaDataService tokenMetaDataService)
    {
        _logger = logger;
        _nftService = nftService;
        _marketPlaceService = marketPlaceService;
        _tokenMetaDataService = tokenMetaDataService;
    }

    [HttpGet]
    [ResponseCache(Duration = 5, VaryByQueryKeys = new string[] { "offset", "limit", "sort" })]
    public async Task<IActionResult> Get(
        [FromQuery, Required, Range(0, 10)] int limit,
        [FromQuery] int offset = 0,
        [FromQuery] Sort sort = Sort.Asc)
    {
        var tokens = new List<CryptoGuitarTokenDTO>();
        var getTokenDataTasks = new List<Task>();
        for (uint tokenId = 0; tokenId < limit; tokenId++)
        {
            getTokenDataTasks.Add(GetTokenDataAsync(tokenId));
        }

        await Task.WhenAll(getTokenDataTasks);

        tokens.AddRange(getTokenDataTasks.Select(t => ((Task<CryptoGuitarTokenDTO>)t).Result));

        return Ok(new GetCryptoGuitarTokensDTO
        {
            Data = sort.Apply(tokens, t => t.TokenId).ToList(),
            Total = 625,
            NextPage = offset + limit < 625,
            Limit = limit,
            Offset = offset
        });
    }

    private async Task<CryptoGuitarTokenDTO> GetTokenDataAsync(uint tokenId)
    {
        var tokenMetaData = await _tokenMetaDataService.GetAsync(new Uri($"CryptoGuitars-{tokenId}-metadata.json", UriKind.Relative));
        var exists = await _nftService.ExistsQueryAsync(tokenId);

        string? tokenOwner = null;
        GetActiveOfferOutputDTO? tokenOffer = null;
        if(exists)
        {
            tokenOwner = await _nftService.OwnerOfQueryAsync(tokenId);
            tokenOffer = await _marketPlaceService.GetActiveOfferQueryAsync(tokenId);
        }

        return new CryptoGuitarTokenDTO
        {
            Owner = tokenOwner,
            TokenId = tokenId,
            OfferPrice = (double?)tokenOffer?.ReturnValue1?.Price ?? 0d,
            IsOffered = tokenOffer?.ReturnValue1?.Active ?? false,
            MetaData = tokenMetaData
        };
    }
}
