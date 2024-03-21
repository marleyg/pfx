param keyVaultName string
param location string
param tenantId string
param scripterId string

param rbacPermissions array = [
      {
        roleDefinitionResourceId: '/providers/Microsoft.Authorization/roleDefinitions/00482a5a-887f-4fb3-b363-3b7fe8e74483' // Key Vault Administrator
        principalId: scripterId 
        principalType: 'User'
        enabled: true
      }
]

resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' = {
  name: keyVaultName
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: tenantId
    accessPolicies: [

    ]
    enableRbacAuthorization: true
  }
}

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = [for item in rbacPermissions: if (item.enabled) {
  name: guid(keyVault.id, item.principalId, item.roleDefinitionResourceId)
  scope: keyVault
  properties: {
    roleDefinitionId: item.roleDefinitionResourceId
    principalId: item.principalId
    principalType: item.principalType
  }
}]

output keyVaultId string = keyVault.id
output keyVaultUri string = keyVault.properties.vaultUri
