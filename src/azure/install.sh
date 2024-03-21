#run these steps to authenticate to Azure and set the subscription and default resource group
az login

# variables for the subscription and resource group
subscriptionId='32886bdb-91b8-4941-96c9-a662977d4455'
resourceGroupName='PathfinderFx'
location='westus2'
keyVaultName='PathfinderFx-kv'
hostOrgName='MyOrg'
environmentType='nonprod'
certPassword='pathfinder'

az account set --subscription $subscriptionId

# Create a resource group and set it as the default
az group create --name $resourceGroupName --location $location
az configure --defaults group=$resourceGroupName

az deployment group create \
--template-file ./main.bicep \
--parameters environmentType=$environmentType location=$location hostOrgName=$hostOrgName \
--resource-group $resourceGroupName \

# generate new self-signed certificates for encryption and signing
../CertGenerator/bin/debug/net8.0/CertGenerator.dll ${certPassword}
#upload the certificates to the key vault
az keyvault certificate import --vault-name ${keyVaultName} --name pfx-encryption-certificate --file encryption-certificate.pfx --password ${certPassword}
az keyvault certificate import --vault-name ${keyVaultName} --name pfx-signing-certificate --file signing-certificate.pfx --password ${certPassword}

#updload the PfxConfigTemplate.json file to the key vault as a secret
az keyvault secret set --vault-name ${keyVaultName} --name PfxConfig --file PfxConfigTemplate.json

#deploy the web app
az webapp deploy --resource-group $resourceGroupName --name ${hostOrgName}'-PathfinderFx' --src-path ../PathfinderFx/bin/release/net8.0/PathfinderFx.zip