// SPDX-License-Identifier: MIT
pragma solidity ^0.8.4;

import "@openzeppelin/contracts/token/ERC721/ERC721.sol";
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721Enumerable.sol";
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "@openzeppelin/contracts/utils/Counters.sol";

contract CryptoGuitarsNFT is ERC721, ERC721Enumerable, ERC721URIStorage, Ownable {

    string internal _uriBaseAddress;

    constructor(string memory uriBaseAddress) ERC721("Crypto Guitars NFT", "CGF")
    {
        _uriBaseAddress = uriBaseAddress;
    }

    function safeMint(address to, uint256 tokenId) public onlyOwner {
        _safeMint(to, tokenId);
        string memory uri = _buildUri(tokenId);
        _setTokenURI(tokenId, uri);
    }

    function exists(uint256 tokenId) public view virtual returns (bool) {
        return super._exists(tokenId);
    }

    // The following functions are overrides required by Solidity.

    function _beforeTokenTransfer(address from, address to, uint256 tokenId)
        internal
        override(ERC721, ERC721Enumerable)
    {
        super._beforeTokenTransfer(from, to, tokenId);
    }

    function _burn(uint256 tokenId) internal override(ERC721, ERC721URIStorage) {
        super._burn(tokenId);
    }

    function tokenURI(uint256 tokenId)
        public
        view
        override(ERC721, ERC721URIStorage)
        returns (string memory)
    {
        return super.tokenURI(tokenId);
    }

    function supportsInterface(bytes4 interfaceId)
        public
        view
        override(ERC721, ERC721Enumerable)
        returns (bool)
    {
        return super.supportsInterface(interfaceId);
    }

    function _buildUri(uint256 tokenId) private view returns(string memory) {
        return string(abi.encodePacked(_uriBaseAddress, "/CryptoGuitars-", Strings.toString(tokenId), "-metadata.json"));
    }
}
