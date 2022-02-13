// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "@nibbstack/erc721/src/contracts/tokens/nf-token-enumerable.sol";
import "@nibbstack/erc721/src/contracts/tokens/nf-token-metadata.sol";
import "@nibbstack/erc721/src/contracts/ownership/ownable.sol";

contract CryptoGuitarNFT is NFTokenEnumerable, NFTokenMetadata, Ownable {

  constructor() {
    nftName = "CryptoGuitar NFT";
    nftSymbol = "CGF";
  }

  function _addNFToken(
    address _to,
    uint256 _tokenId
  )
    internal
    override (NFTokenEnumerable, NFToken)
  {
    super._addNFToken(_to, _tokenId);
  }

  function _burn(
    uint256 _tokenId
  )
    internal
    override (NFTokenEnumerable, NFTokenMetadata)
  {
    super._burn(_tokenId);
  }

  function _mint(
    address _to,
    uint256 _tokenId
  )
    internal
    override (NFTokenEnumerable, NFToken)
  {
    super._mint(_to, _tokenId);
  }

  function _getOwnerNFTCount(
    address _owner
  )
    internal
    view
    override (NFTokenEnumerable, NFToken)
    returns(uint256)
  {
    return super._getOwnerNFTCount(_owner);
  }

  function _removeNFToken(
    address _from,
    uint256 _tokenId
  )
    internal
    override (NFTokenEnumerable, NFToken)
  {
    super._removeNFToken(_from, _tokenId);
  }

  function mint(address _to, uint256 _tokenId, string calldata _uri) public virtual payable
  {
    require(msg.value >= 0.05 ether, "Not enough ETH sent!");

    super._mint(_to, _tokenId);
    super._setTokenUri(_tokenId, _uri);
  }
}