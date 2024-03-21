param location string
param hostOrgName string 
param appServiceAppName string = '${hostOrgName}-PathfinderFx'
param keyVaultName string = '${hostOrgName}-pathfinder-kv'

@allowed([
  'nonprod'
  'prod'
])
param environmentType string

var appServicePlanName = '${hostOrgName}-pathfinder-plan'

module appService 'modules/appService.bicep' = {
  name: 'appService'
  params: {
    location: location
    appServicePlanName: appServicePlanName
    appServiceAppName: appServiceAppName
    environmentType: environmentType
  }
}

module keyVault 'modules/keyVault.bicep' = {
  name: 'keyVault'
  params: {
    location: location
    keyVaultName: keyVaultName
    tenantId: appService.outputs.tenantId
  }
}

module policy 'modules/policy.bicep' = {
  name: 'policy'
  params: {
    webappPrincipleId: appService.outputs.appServicePrincipalId
    keyVaultName: keyVaultName
    keyVaultUri: keyVault.outputs.keyVaultUri
    tenantId: appService.outputs.tenantId
    webAppName: appServiceAppName
  }
}
