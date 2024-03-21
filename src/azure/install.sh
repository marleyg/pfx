#run these steps to authenticate to Azure and set the subscription and default resource group
az login

# variables for the subscription and resource group
subscriptionId='32886bdb-91b8-4941-96c9-a662977d4455'
location='westus2'
hostOrgName='testorg3'
environmentType='nonprod'
certPassword='pathfinder'
resourceGroupName=${hostOrgName}'-PathfinderFx'
keyVaultName=${hostOrgName}'-PathfinderFx-kv'

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
dotnet ../CertGenerator/bin/debug/net8.0/CertGenerator.dll ${certPassword}
#upload the certificates to the key vault
az keyvault certificate import --vault-name ${keyVaultName} --name pfx-encryption-certificate --file '../CertGenerator/bin/debug/net8.0/encryption-certificate.pfx' --password ${certPassword}
az keyvault certificate import --vault-name ${keyVaultName} --name pfx-signing-certificate --file '../CertGenerator/bin/debug/net8.0/signing-certificate.pfx' --password ${certPassword}

#updload the PfxConfigTemplate.json file to the key vault as a secret
az keyvault secret set --vault-name ${keyVaultName} --name PfxConfig --file PfxConfigTemplate.json

#deploy the web app
az webapp deploy --resource-group $resourceGroupName --name ${hostOrgName}'-PathfinderFx' --src-path ../PathfinderFx/bin/release/net8.0/PathfinderFx.zip