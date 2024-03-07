#run these steps to authenticate to Azure and set the subscription and default resource group
az login

# variables for the subscription and resource group
subscriptionId='32886bdb-91b8-4941-96c9-a662977d4455'
resourceGroupName='PathfinderFx'
location='westus2'
keyVaultName='PathfinderFx-kv'
hostOrgName='MyOrg'
environmentType='nonprod'

az account set --subscription $subscriptionId

# Create a resource group and set it as the default
az group create --name $resourceGroupName --location $location
az configure --defaults group=$resourceGroupName

az keyvault create --name $keyVaultName --location $location --enable-rbac-authorization true 
#get the keyvault url
keyVaultUrl=$(az keyvault show --name $keyVaultName --query properties.vaultUri -o tsv)

az deployment group create \
--template-file ../main.bicep \
--parameters environmentType=$environmentType location=$location hostOrgName=$hostOrgName keyVaultUrl=$keyVaultUrl \
--resource-group $resourceGroupName \

az webapp deploy --resource-group $resourceGroupName --name ${hostOrgName}'-PathfinderFx' --src-path ../package/PathfinderFx.zip