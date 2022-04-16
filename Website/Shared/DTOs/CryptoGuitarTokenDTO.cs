namespace CryptoGuitars.Shared.DTOs;

public class CryptoGuitarTokenDTO
{
    public uint TokenId { get; set; }

    public string? Owner { get; set; }

    public bool IsOffered { get; set; }

    public double? OfferPrice { get; set; }

    public TokenMetaDataDTO? MetaData { get; set; }
}
