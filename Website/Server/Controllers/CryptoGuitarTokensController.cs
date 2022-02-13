using Microsoft.AspNetCore.Mvc;
using CryptoGuitars.Shared.DTOs;
using CryptoGuitars.Server.Services;
using CryptoGuitars.Contracts.CryptoGuitarNFT;
using CryptoGuitars.Shared.Enums;
using CryptoGuitars.Shared.Extensions;

namespace CryptoGuitars.Server.Controllers;

[ApiController]
[Route("api/v1/crypto-guitars-tokens")]
public class CryptoGuitarTokensController : ControllerBase
{
    private readonly ILogger<CryptoGuitarTokensController> _logger;
    private readonly CryptoGuitarNFTService _service;
    private readonly ITokenMetaDataService _tokenMetaDataService;

    public CryptoGuitarTokensController(
        ILogger<CryptoGuitarTokensController> logger,
        CryptoGuitarNFTService service,
        ITokenMetaDataService tokenMetaDataService)
    {
        _logger = logger;
        _service = service;
        _tokenMetaDataService = tokenMetaDataService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery]int offset = 0,
        [FromQuery]int limit = 10,
        [FromQuery]Sort sort = Sort.Desc)
    {
        var totalSupply = (int)(await _service.TotalSupplyQueryAsync());
        if(limit > totalSupply)
        {
            limit = (int)totalSupply;
        }

        var tokens = new List<CryptoGuitarTokenDTO>();
        var getTokenDataTasks = new List<Task>();
        for (int i = 0; i < limit; i++)
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

    private async Task<CryptoGuitarTokenDTO> GetTokenDataAsync(int i)
    {
        var tokenId = await _service.TokenByIndexQueryAsync(i);
        var tokenOwner = await _service.OwnerOfQueryAsync(tokenId);
        var tokenUri = new Uri(await _service.TokenURIQueryAsync(tokenId));
        var tokenMetaData = await _tokenMetaDataService.GetAsync(tokenUri);

        return new CryptoGuitarTokenDTO
        {
            Id = (int)tokenId,
            Uri = tokenUri,
            Owner = tokenOwner,
            Index = i,
            MetaData = tokenMetaData
        };
    }
}
