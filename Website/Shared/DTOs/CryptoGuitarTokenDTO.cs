namespace CryptoGuitars.Shared.DTOs;

public class CryptoGuitarTokenDTO
{
    public uint Id { get; set; }

    public uint? Index { get; set; }

    public string? Owner { get; set; }

    public Uri? Uri { get; set; }

    public bool IsOffered { get; set; }

    public decimal OfferPrice { get; set; }

    public TokenMetaDataDTO? MetaData { get; set; }
}
