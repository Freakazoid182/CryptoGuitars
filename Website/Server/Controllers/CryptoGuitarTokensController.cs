using Microsoft.AspNetCore.Mvc;
using CryptoGuitars.Shared.DTOs;
using CryptoGuitars.Server.Services;
using CryptoGuitars.Contracts.CryptoGuitarsNFT;
using CryptoGuitars.Shared.Enums;
using CryptoGuitars.Shared.Extensions;
using System.ComponentModel.DataAnnotations;
using CryptoGuitars.Contracts.CryptoGuitarsMarketPlace;

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
        [FromQuery] Sort sort = Sort.Desc)
    {

        var totalSupply = (int)(await _nftService.TotalSupplyQueryAsync());
        if (limit > totalSupply)
        {
            return BadRequest(new ValidationResult("'limit' cannot be higher than supply", new [] { "limit" }));
        }

        var tokens = new List<CryptoGuitarTokenDTO>();
        var getTokenDataTasks = new List<Task>();
        for (uint i = 0; i < limit; i++)
        {
            getTokenDataTasks.Add(GetTokenDataAsync(i));
        }

        await Task.WhenAll(getTokenDataTasks);

        tokens.AddRange(getTokenDataTasks.Select(t => ((Task<CryptoGuitarTokenDTO>)t).Result));

        return Ok(new GetCryptoGuitarTokensDTO
        {
            Data = sort.Apply(tokens, t => t.Id).ToList(),
            Total = totalSupply,
            NextPage = offset + limit < totalSupply,
            Limit = limit,
            Offset = offset
        });
    }

    private async Task<CryptoGuitarTokenDTO> GetTokenDataAsync(uint i)
    {
        var tokenId = await _nftService.TokenByIndexQueryAsync(i);
        var tokenOwner = await _nftService.OwnerOfQueryAsync(tokenId);
        var tokenUri = new Uri(await _nftService.TokenURIQueryAsync(tokenId));
        var tokenOffer = await _marketPlaceService.GetActiveOfferQueryAsync(tokenId);
        var tokenMetaData = await _tokenMetaDataService.GetAsync(tokenUri);

        return new CryptoGuitarTokenDTO
        {
            Id = (uint)tokenId,
            Uri = tokenUri,
            Owner = tokenOwner,
            Index = i,
            OfferPrice = (decimal)tokenOffer.ReturnValue1.Price,
            IsOffered = tokenOffer.ReturnValue1.Active,
            MetaData = tokenMetaData
        };
    }
}
