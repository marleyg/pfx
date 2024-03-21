param webappPrincipleId string
param webappPrincipleTenantId string
param keyVaultName string
param keyVaultUri string
param tenantId string
param webAppName string
param scripterId string

resource keyVaultAdminAccessPolicy 'Microsoft.KeyVault/vaults/accessPolicies@2023-07-01' = {
  name: '${keyVaultName}/addAdmin'
  properties: {
    accessPolicies: [
      {
        tenantId: tenantId
        objectId: scripterId
        permissions: {
          secrets: [
            'all'    
          ]
          certificates: [
            'all'
          ]
        }
      }
    ]
  }
}

resource keyVaultAccessPolicy 'Microsoft.KeyVault/vaults/accessPolicies@2023-07-01' = {
  name: '${keyVaultName}/add'
  properties: {
    accessPolicies: [
      {
        tenantId: webappPrincipleTenantId
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
  name: '${webAppName}/kvenv'
  properties: {
    appSettings: [
      {
        name: 'AKV_URI'
        value: keyVaultUri
      }
    ]
  }
}
