using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace CryptoGuitars.Contracts.CryptoGuitarsMarketPlace.ContractDefinition
{
    public partial class Offer : OfferBase { }

    public class OfferBase 
    {
        [Parameter("address", "seller", 1)]
        public virtual string Seller { get; set; }
        [Parameter("uint256", "price", 2)]
        public virtual BigInteger Price { get; set; }
        [Parameter("uint256", "index", 3)]
        public virtual BigInteger Index { get; set; }
        [Parameter("uint256", "tokenId", 4)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("bool", "active", 5)]
        public virtual bool Active { get; set; }
    }
}
