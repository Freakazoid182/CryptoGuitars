param location string = resourceGroup().location
param sku string = 'F1' // The SKU of App Service Plan

@secure()
param web3BaseUrl string

@allowed([
  'stg'
  'prd'
])
param environment string

param name string = '${environment}-crypto-guitars-api-server'

resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: name
  location: location
  sku: {
    name: sku
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

resource appService 'Microsoft.Web/sites@2020-06-01' = {
  name: name
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      appSettings:[
        {
          name: 'DOTNET_ENVIRONMENT'
          value: environment == 'stg' ? 'Staging' : 'Production'
        }
        {
          name: 'Web3__BaseUrl'
          value: web3BaseUrl
        }
      ]
      linuxFxVersion: 'DOTNETCORE|6.0'
    }
    httpsOnly: true
  }
}
