namespace CryptoGuitars.Client.Models;

using MetaMask.Blazor.Enums;

public class MetaMaskState
{
    public MetaMaskState(bool hasMetaMask, string selectedAddress, Chain chain)
    {
        HasMetaMask = hasMetaMask;
        SelectedAddress = selectedAddress;
        Chain = chain;
    }

    public bool HasMetaMask { get; set; }

    public string SelectedAddress { get; set; }

    public Chain Chain { get; set; }
}