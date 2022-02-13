namespace CryptoGuitars.Shared.DTOs;

public class GetCryptoGuitarTokensDTO : PaginationDTO<CryptoGuitarTokenDTO>
{
    public override IEnumerable<CryptoGuitarTokenDTO>? Data { get; set; }
}
