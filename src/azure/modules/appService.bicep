param location string
param appServiceAppName string
param keyVaultUrl string

@allowed([
  'nonprod'
  'prod'
])
param environmentType string

param appServicePlanName string
var appServicePlanSkuName = (environmentType == 'prod') ? 'P2v3' : 'F1'


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
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: 'Production'
        }
        {
            name: 'ENCRYPTION_CERTIFICATE_NAME'
            value: 'pfx-encryption-certificate'
        }
        {
            name: 'SIGNING_CERTIFICATE_NAME'
            value: 'pfx-signing-certificate'
        }
        {
            name: 'AKS_URI'
            value: keyVaultUrl
        }
        {
            name: 'PFX_CONFIG_SECRET_NAME'
            value: 'pfx-config'
        }
      ]
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
  kind: 'app,linux'
}

output appServiceAppHostName string = appServiceApp.properties.defaultHostName
