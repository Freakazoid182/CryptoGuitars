namespace CryptoGuitars.Shared.DTOs;

public class CryptoGuitarTokenDTO
{
    public int? Id { get; set; }

    public int? Index { get; set; }

    public string? Owner { get; set; }

    public Uri? Uri { get; set; }

    public TokenMetaDataDTO? MetaData { get; set; }
}
