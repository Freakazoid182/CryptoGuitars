// SPDX-License-Identifier: MIT
pragma solidity ^0.8.4;

import "./CryptoGuitarsNFT.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "@openzeppelin/contracts/utils/Strings.sol";

contract CryptoGuitarsMarketPlace is Ownable {

    CryptoGuitarsNFT internal _cryptoGuitarsNFTContract;

    address payable internal _feeReceiverAddress;

    string internal _uriBaseAddress;

    uint256 internal _newTokenPrice = 0.05 ether;

    struct Offer {
        address seller;
        uint256 price;
        uint256 index;
        uint256 tokenId;
        bool active;
    }

    Offer[] internal offers;

    mapping(uint256 => Offer) internal tokenIdToOffer;

    constructor(
        address cryptoGuitarsNFTContractAddress,
        address payable feeReceiverAddress,
        string memory uriBaseAddress) {
        _cryptoGuitarsNFTContract = CryptoGuitarsNFT(cryptoGuitarsNFTContractAddress);
        _feeReceiverAddress = feeReceiverAddress;
        _uriBaseAddress = uriBaseAddress;
    }

    modifier onlyTokenOwner(uint256 _tokenId) {
        require(
            msg.sender == _cryptoGuitarsNFTContract.ownerOf(_tokenId),
            "not token owner"
        );
        _;
    }

    modifier activeOffer(uint256 _tokenId) {
        require(hasActiveOffer(_tokenId), "offer not active");
        _;
    }

    modifier noActiveOffer(uint256 _tokenId) {
        require(!hasActiveOffer(_tokenId), "duplicate offer");
        _;
    }

    function hasActiveOffer(uint256 _tokenId) public view returns (bool) {
        return tokenIdToOffer[_tokenId].active;
    }

    function getActiveOffer(uint256 _tokenId) public view returns (Offer memory) {
        return tokenIdToOffer[_tokenId];
    }

    modifier marketApproved() {
        require(
            _cryptoGuitarsNFTContract.isApprovedForAll(msg.sender, address(this)),
            "market must be approved operator"
        );
        _;
    }

    // Create a new token (mint)
    function createNewToken(address to) external payable {
        require(msg.value == _newTokenPrice, "ether value is incorrect");

        // TokenId will be the same as the total token supply
        uint256 tokenId = _cryptoGuitarsNFTContract.totalSupply();

        string memory uri = _buildUri(tokenId);

        // Mint a new token
        _cryptoGuitarsNFTContract.safeMint(to, uri);

        // Sending the complete value of the token to a fee receiver
        _feeReceiverAddress.transfer(msg.value);
    }

    // Offer a token
    function offerToken(uint256 tokenId, uint256 price)
        external
        marketApproved
        onlyTokenOwner(tokenId)
        noActiveOffer(tokenId) {

        Offer memory offer = Offer(
            msg.sender,
            price,
            offers.length,
            tokenId,
            true
        );

        offers.push(offer);

        tokenIdToOffer[tokenId] = offer;
    }

    function buyToken(uint256 _tokenId) external payable activeOffer(_tokenId) {
        Offer memory offer = tokenIdToOffer[_tokenId];
        require(msg.value == offer.price, "payment must be exact");

        _executeOffer(offer);

        // tranfer token ownership
        _cryptoGuitarsNFTContract.transferFrom(offer.seller, msg.sender, _tokenId);

        // emit event
        // emit MarketTransaction("Buy", msg.sender, _tokenId);
    }

    function _executeOffer(Offer memory offer) private {
        // To prevent re-entry attack
        _setOfferInactive(offer.tokenId);

        if (offer.price > 0) {
            payable(address(offer.seller)).transfer(offer.price);
        }
    }

    function _setOfferInactive(uint256 _tokenId) internal {
        offers[tokenIdToOffer[_tokenId].index].active = false;
        delete tokenIdToOffer[_tokenId];
    }

    function _buildUri(uint256 tokenId) private view returns(string memory) {
        return string(abi.encodePacked(_uriBaseAddress, "/metadata/CryptoGuitars-", Strings.toString(tokenId), "-metadata.json"));
    }
}
