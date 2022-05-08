targetScope = 'subscription'

param location string = 'northeurope'

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: 'cg-site'
  location: location
}

module staticSite 'staticWebstorageAccount.bicep' = {
  scope: resourceGroup
  name: 'storageAccountDeployment'
  params: {
    location: location
  }
}
