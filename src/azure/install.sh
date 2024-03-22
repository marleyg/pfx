#run these steps to authenticate to Azure and set the subscription and default resource group
az login

# variables for the subscription and resource group
subscriptionId='32886bdb-91b8-4941-96c9-a662977d4455'
location='westus2'
hostOrgName='msft2'
environmentType='nonprod'
certPassword='pathfinder'
resourceGroupName=${hostOrgName}'-pathfinderfx'
keyVaultName=${hostOrgName}'-pathfinderfx-kv'

az account set --subscription $subscriptionId

# Create a resource group and set it as the default
az group create --name $resourceGroupName --location $location
az configure --defaults group=$resourceGroupName

# Get your user id to use as the scripterId to assign policy for the key vault
scripterId=$(az ad signed-in-user show --query id -o tsv)

az deployment group create \
--template-file ./main.bicep \
--parameters environmentType=$environmentType location=$location hostOrgName=$hostOrgName scripterId=$scripterId \
--resource-group $resourceGroupName \

# generate new self-signed certificates for encryption and signing
az keyvault certificate create --vault-name ${keyVaultName} --name pfx-encryption-certificate --policy "$(az keyvault certificate get-default-policy)"
az keyvault certificate create --vault-name ${keyVaultName} --name pfx-signing-certificate --policy "$(az keyvault certificate get-default-policy)"

#deploy the web app
az webapp deploy --resource-group $resourceGroupName --name ${hostOrgName}'-PathfinderFx' --src-path ../PathfinderFx/bin/release/net8.0/PathfinderFx.zip