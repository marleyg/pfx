#run these steps to authenticate to Azure and set the subscription and default resource group
#az login

# variables for the subscription and resource group
subscriptionId='32886bdb-91b8-4941-96c9-a662977d4455'
location='westus2'
hostOrgName='testorg5'
environmentType='nonprod'
certPassword='pathfinder'
resourceGroupName=${hostOrgName}'-pathfinderfx'
keyVaultName=${hostOrgName}'-pathfinderfx-kv'



az account set --subscription $subscriptionId

az configure --defaults group=$resourceGroupName


# generate new self-signed certificates for encryption and signing
#dotnet ../CertGenerator/bin/debug/net8.0/CertGenerator.dll ${certPassword}

#upload the certificates to the key vault

echo "Key Vault Name: ${keyVaultName}"
az keyvault certificate create --vault-name ${keyVaultName} --name pfx-encryption-certificate --policy "$(az keyvault certificate get-default-policy)"
az keyvault certificate create --vault-name ${keyVaultName} --name pfx-signing-certificate --policy "$(az keyvault certificate get-default-policy)"

#az webapp deploy --resource-group $resourceGroupName --name ${hostOrgName}'-PathfinderFx' --src-path ../PathfinderFx/bin/release/net8.0/PathfinderFx.zip
#az keyvault certificate import --file encryption-certificate.pfx --name pfx-encryption-certificate --password ${certPassword} --vault-name ${keyVaultName}
#az keyvault certificate import --vault-name ${keyVaultName} --name pfx-signing-certificate --file '../CertGenerator/bin/debug/net8.0/signing-certificate.pfx' --password ${certPassword}

#updload the PfxConfigTemplate.json file to the key vault as a secret
#az keyvault secret set --vault-name ${keyVaultName} --name PfxConfig --file PfxConfigTemplate.json
