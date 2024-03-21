param location string
param appServiceAppName string
param appServicePlanName string

@allowed([
  'nonprod'
  'prod'
])
param environmentType string


var appServicePlanSkuName = (environmentType == 'prod') ? 'P2v3' : 'B1'


resource appServicePlan 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: appServicePlanName
  location: location
  kind: 'linux'
  properties: {
    reserved: true
  }
  sku: {
    name: appServicePlanSkuName
  }
}

resource appServiceApp 'Microsoft.Web/sites@2023-01-01' = {
  name: appServiceAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true 
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|8.0'
      acrUseManagedIdentityCreds: true
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
  kind: 'app,linux'
}

output appServiceAppHostName string = appServiceApp.properties.defaultHostName
output appServiceAppId string = appServiceApp.id
output appServicePrincipalId string = appServiceApp.identity.principalId
output appServicePrincipleTenantId string = appServiceApp.identity.tenantId
