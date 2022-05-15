// We require the Hardhat Runtime Environment explicitly here. This is optional
// but useful for running the script in a standalone fashion through `node <script>`.
//
// When running the script with `npx hardhat run <script>` you'll find the Hardhat
// Runtime Environment's members available in the global scope.
import { ethers } from "hardhat";

import * as dotenv from "dotenv";
import { string } from "hardhat/internal/core/params/argumentTypes";

dotenv.config();

async function main() {
  // Hardhat always runs the compile task when running scripts with its command
  // line interface.
  //
  // If this script is run directly using `node` you may want to call compile
  // manually to make sure everything is compiled
  // await hre.run('compile');

  // We get the contract to deploy
  const GuitarsNFT = await ethers.getContractFactory("CryptoGuitarsNFT");
  const guitarsNFT = await GuitarsNFT.deploy("https://ipfs.io/QmXgYAq8vPzjACYpJVgNdUxpMpXN6HfAV7zUDrMqbtofos");

  await guitarsNFT.deployed();

  console.log("CryptoGuitars NFT deployed to:", guitarsNFT.address);

  const GuitarsMarketPlace = await ethers.getContractFactory("CryptoGuitarsMarketPlace");
  const guitarsMarketPlace = await GuitarsMarketPlace.deploy(
    guitarsNFT.address,
    process.env.PUBLIC_KEY !== undefined ? process.env.PUBLIC_KEY : "");

  await guitarsMarketPlace.deployed();

  console.log("CryptoGuitars Market Place deployed to:", guitarsMarketPlace.address);

  guitarsNFT.transferOwnership(guitarsMarketPlace.address);

  console.log("Transferred ownership of the NFT contract to the marketplace contract");
}

// We recommend this pattern to be able to use async/await everywhere
// and properly handle errors.
main().catch((error) => {
  console.error(error);
  process.exitCode = 1;
});
