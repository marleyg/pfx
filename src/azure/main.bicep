param location string
param hostOrgName string 
param appServiceAppName string = '${hostOrgName}-PathfinderFx'
param keyVaultName string
param tenantId string = subscription().tenantId
@description('Specifies the object ID of a user, service principal or security group in the Azure Active Directory tenant for the vault. The object ID must be unique for the list of access policies. Get it by using Get-AzADUser or Get-AzADServicePrincipal cmdlets.')
param scripterId string

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
    tenantId: tenantId
    scripterId: scripterId
    webappPrincipleId: appService.outputs.appServicePrincipalId
  }
}

module appServiceConfig 'modules/appServiceConfig.bicep' = {
  name: 'config'
  params: {
    keyVaultUri: keyVault.outputs.keyVaultUri
    webAppName: appServiceAppName
  }
}

