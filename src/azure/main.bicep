param location string
param hostOrgName string 
param storageAccountName string = 'massiv${toLower(hostOrgName)}storage'
param appServiceAppName string = '${hostOrgName}-Beacon'
param keyVaultUrl string 

@allowed([
  'nonprod'
  'prod'
])
param environmentType string = 'nonprod'

var storageAccountSkuName = (environmentType == 'prod') ? 'Standard_GRS' : 'Standard_LRS'

var appServicePlanName = 'massiv-beacon-plan'

resource storageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: storageAccountName
  location: location
  kind: 'StorageV2'
  sku: {
    name: storageAccountSkuName
  }
  properties: {
    accessTier: 'Hot'
    minimumTlsVersion: 'TLS1_2'
    isHnsEnabled: true
  }
}

module appService 'modules/appService.bicep' = {
  name: 'appService'
  params: {
    location: location
    appServicePlanName: appServicePlanName
    appServiceAppName: appServiceAppName
    environmentType: environmentType
    keyVaultUrl: keyVaultUrl
  }
}

output appServiceAppHostName string = appService.outputs.appServiceAppHostName

