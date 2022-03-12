using CryptoGuitars.Contracts.CryptoGuitarsNFT;
using Microsoft.AspNetCore.Mvc;

namespace CryptoGuitars.Server.Controllers;

[ApiController]
[Route("api/v1/crypto-guitar-contract")]
public class CryptoGuitarContractController : ControllerBase
{
    private readonly ILogger<CryptoGuitarContractController> _logger;
    private readonly CryptoGuitarsNFTService _service;

    public CryptoGuitarContractController(
        ILogger<CryptoGuitarContractController> logger,
        CryptoGuitarsNFTService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("name")]
    [ResponseCache(Duration = 86400)]
    public async Task<IActionResult> GetNameAsync()
    {
        var name = await _service.NameQueryAsync();
        return Ok(name);
    }

    [HttpGet("owner-of/{id:long}")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetBalanceOfAsync(long id)
    {
        var owner = await _service.OwnerOfQueryAsync(id);
        return Ok(owner);
    }

    [HttpGet("balance-of/{owner}")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> GetBalanceOfAsync(string owner)
    {
        var balance = await _service.BalanceOfQueryAsync(owner);
        return Ok((int)balance);
    }

    [HttpGet("symbol")]
    [ResponseCache(Duration = 86400)]
    public async Task<IActionResult> SymbolAsync()
    {
        var symbol = await _service.SymbolQueryAsync();
        return Ok(symbol);
    }

    [HttpGet("total-supply")]
    [ResponseCache(Duration = 1)]
    public async Task<IActionResult> TotalSupply()
    {
        var totalSupply = await _service.TotalSupplyQueryAsync();
        return Ok((int)totalSupply);
    }
}
