param webappPrincipleId string
param keyVaultName string
param keyVaultUri string
param tenantId string
param webAppName string

resource keyVaultAccessPolicy 'Microsoft.KeyVault/vaults/accessPolicies@2023-07-01' = {
  name: keyVaultName
  properties: {
    accessPolicies: [
      {
        tenantId: tenantId
        objectId: webappPrincipleId
        permissions: {
          secrets: [
            'get'
            'list'
          ]
          certificates: [
            'get'
            'list'
          ]
        }
      }
    ]
  }
}

resource webAppConfig 'Microsoft.Web/sites/config@2023-01-01' = {
  name: webAppName
  properties: {
    appSettings: [
      {
        name: 'AKV_URI'
        value: keyVaultUri
      }
    ]
  }
}
