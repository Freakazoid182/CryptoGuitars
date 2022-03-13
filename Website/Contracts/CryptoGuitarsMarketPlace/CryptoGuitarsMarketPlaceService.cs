using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using CryptoGuitars.Contracts.CryptoGuitarsMarketPlace.ContractDefinition;

namespace CryptoGuitars.Contracts.CryptoGuitarsMarketPlace
{
    public partial class CryptoGuitarsMarketPlaceService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, CryptoGuitarsMarketPlaceDeployment cryptoGuitarsMarketPlaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<CryptoGuitarsMarketPlaceDeployment>().SendRequestAndWaitForReceiptAsync(cryptoGuitarsMarketPlaceDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, CryptoGuitarsMarketPlaceDeployment cryptoGuitarsMarketPlaceDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<CryptoGuitarsMarketPlaceDeployment>().SendRequestAsync(cryptoGuitarsMarketPlaceDeployment);
        }

        public static async Task<CryptoGuitarsMarketPlaceService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, CryptoGuitarsMarketPlaceDeployment cryptoGuitarsMarketPlaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, cryptoGuitarsMarketPlaceDeployment, cancellationTokenSource);
            return new CryptoGuitarsMarketPlaceService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public CryptoGuitarsMarketPlaceService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> BuyTokenRequestAsync(BuyTokenFunction buyTokenFunction)
        {
             return ContractHandler.SendRequestAsync(buyTokenFunction);
        }

        public Task<TransactionReceipt> BuyTokenRequestAndWaitForReceiptAsync(BuyTokenFunction buyTokenFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyTokenFunction, cancellationToken);
        }

        public Task<string> BuyTokenRequestAsync(BigInteger tokenId)
        {
            var buyTokenFunction = new BuyTokenFunction();
                buyTokenFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAsync(buyTokenFunction);
        }

        public Task<TransactionReceipt> BuyTokenRequestAndWaitForReceiptAsync(BigInteger tokenId, CancellationTokenSource cancellationToken = null)
        {
            var buyTokenFunction = new BuyTokenFunction();
                buyTokenFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyTokenFunction, cancellationToken);
        }

        public Task<string> CreateNewTokenRequestAsync(CreateNewTokenFunction createNewTokenFunction)
        {
             return ContractHandler.SendRequestAsync(createNewTokenFunction);
        }

        public Task<TransactionReceipt> CreateNewTokenRequestAndWaitForReceiptAsync(CreateNewTokenFunction createNewTokenFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createNewTokenFunction, cancellationToken);
        }

        public Task<string> CreateNewTokenRequestAsync(string to)
        {
            var createNewTokenFunction = new CreateNewTokenFunction();
                createNewTokenFunction.To = to;
            
             return ContractHandler.SendRequestAsync(createNewTokenFunction);
        }

        public Task<TransactionReceipt> CreateNewTokenRequestAndWaitForReceiptAsync(string to, CancellationTokenSource cancellationToken = null)
        {
            var createNewTokenFunction = new CreateNewTokenFunction();
                createNewTokenFunction.To = to;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createNewTokenFunction, cancellationToken);
        }

        public Task<bool> HasActiveOfferQueryAsync(HasActiveOfferFunction hasActiveOfferFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HasActiveOfferFunction, bool>(hasActiveOfferFunction, blockParameter);
        }

        
        public Task<bool> HasActiveOfferQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null)
        {
            var hasActiveOfferFunction = new HasActiveOfferFunction();
                hasActiveOfferFunction.TokenId = tokenId;
            
            return ContractHandler.QueryAsync<HasActiveOfferFunction, bool>(hasActiveOfferFunction, blockParameter);
        }

        public Task<string> OfferTokenRequestAsync(OfferTokenFunction offerTokenFunction)
        {
             return ContractHandler.SendRequestAsync(offerTokenFunction);
        }

        public Task<TransactionReceipt> OfferTokenRequestAndWaitForReceiptAsync(OfferTokenFunction offerTokenFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(offerTokenFunction, cancellationToken);
        }

        public Task<string> OfferTokenRequestAsync(BigInteger tokenId, BigInteger price)
        {
            var offerTokenFunction = new OfferTokenFunction();
                offerTokenFunction.TokenId = tokenId;
                offerTokenFunction.Price = price;
            
             return ContractHandler.SendRequestAsync(offerTokenFunction);
        }

        public Task<TransactionReceipt> OfferTokenRequestAndWaitForReceiptAsync(BigInteger tokenId, BigInteger price, CancellationTokenSource cancellationToken = null)
        {
            var offerTokenFunction = new OfferTokenFunction();
                offerTokenFunction.TokenId = tokenId;
                offerTokenFunction.Price = price;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(offerTokenFunction, cancellationToken);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }

        
        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public Task<string> RenounceOwnershipRequestAsync(RenounceOwnershipFunction renounceOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(renounceOwnershipFunction);
        }

        public Task<string> RenounceOwnershipRequestAsync()
        {
             return ContractHandler.SendRequestAsync<RenounceOwnershipFunction>();
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(RenounceOwnershipFunction renounceOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceOwnershipFunction, cancellationToken);
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<RenounceOwnershipFunction>(null, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(string newOwner)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }
    }
}
