namespace CryptoGuitars.Shared.DTOs;

public class PaginationDTO<TData>
{
    public virtual IEnumerable<TData>? Data { get; set; }

    public int Total { get; set; }

    public bool NextPage { get; set; }

    public int Offset { get; set; }

    public int Limit { get; set; }
}
