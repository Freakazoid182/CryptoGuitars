param location string = resourceGroup().location

@allowed([
  'stg'
  'prd'
])
param environment string

param name string = '${environment}cryptoguitarsweb'

resource storageAccount 'Microsoft.Storage/storageAccounts@2021-09-01' = {
  name: name
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    accessTier: 'Hot'
  }
}
