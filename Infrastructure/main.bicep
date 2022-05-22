targetScope = 'subscription'

@allowed([
  'stg'
  'prd'
])
param environment string

param location string = 'northeurope'

@secure()
param web3BaseUrl string

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: '${environment}-cg-site'
  location: location
}

module clientSite 'staticWebstorageAccount.bicep' = {
  scope: resourceGroup
  name: 'storageAccountDeployment'
  params: {
    environment: environment
    location: location
  }
}

module server 'appServiceLinux.bicep' = {
  scope: resourceGroup
  name: 'appServiceLinuxDeployment'
  params: {
    environment: environment
    location: location
    web3BaseUrl: web3BaseUrl
  }
}
