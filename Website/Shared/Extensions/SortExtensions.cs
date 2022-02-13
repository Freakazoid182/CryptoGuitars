namespace CryptoGuitars.Shared.Extensions;

using CryptoGuitars.Shared.Enums;

public static class SortExtensions
{
    public static IEnumerable<TCollection> Apply<TCollection, TKey>(
        this Sort sort,
        IEnumerable<TCollection> collection,
        Func<TCollection, TKey> keySelector)
    {
        if(sort == Sort.Asc)
        {
            return collection.OrderBy(keySelector);
        }
        else if(sort == Sort.Desc)
        {
            return collection.OrderByDescending(keySelector);
        }

        throw new ArgumentOutOfRangeException(nameof(sort));
    }
}
