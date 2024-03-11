param location string
param hostOrgName string 
param appServiceAppName string = '${hostOrgName}-PathfinderFx'
param keyVaultUrl string 

@allowed([
  'nonprod'
  'prod'
])
param environmentType string = 'nonprod'

var appServicePlanName = '${hostOrgName}-pathfinder-plan'

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

