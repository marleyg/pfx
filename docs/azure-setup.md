# Setting up the PathfinderFx Host in Azure

Installing the PathfinderFx Host in Azure involves creating the necessary Azure resources and then installing the host. This install is the minimum non-enterprise configuration, and is intended to be used for testing and development purposes. The installation process is automated using a series of scripts, which will create the necessary Azure resources and install the PathfinderFx. The installation process will require some manual configuration to complete the installation.

This code base can switch from using a local accounts database to using Azure AD B2C for authentication. This is not covered in this document, but the code base is set up to use Azure AD B2C for authentication.

## Prerequisites

You will need to have an Azure subscription and an administrator account that has the permissions needed to be able to deploy the PathfinderFx.

## Overview

You will run a series of scripts to install the PathfinderFx Host:

- A `install.sh` script that is used to install the PathfinderFx, using the default settings. If you would like to customize the installation, you can modify the `install.sh` script .
- A `PfxConfig.json` file that contains the default host configuration, including the accounts you would like to have access to the API. This file is modified locally, to be updated from time to time when configuration changes are made, i.e., a new account for access is added. The contents of this file are then copied and pasted into an Azure Key Vault secret, which is then used by the PathfinderFx for its configuration.
  - For an account to be authorized for accessing the API, it will need to have the API permissions. This must be done by adding it to the configuration file and then updating the Key Vault secret, *before* the deployment has started. Meaning, if the account is created already and you change the configuration, it will not pickup the change as the account already exists. You can re-deploy the solution to reset the accounts database.

  ```json
        {
            "ClientId": "ctest2",
            "ClientSecret": "some-secret",
            "DisplayName": "Conformance test application 2",
            "Permissions": [
                "api"
            ]
        }
  ```

The install script will automate most of the installation process, including the creation of the Azure resources, the installation of the PathfinderFx, but additional configuration is required to complete the installation.

## Installation Steps

The installation process is as follows:

1. Update the `install.sh` with the `subscriptionId`, `hostOrgName` and `location` (currently set to `westus2`, but choose a location that makes sense for your organization) for the Azure subscription, your organization name and location that the PathfinderFx will be installed in. This can be found in the Azure portal by navigating to the subscription and copying the Subscription ID.
2. Run the `install.sh` script from the [azure](../src/azure/) folder to install the core infrastructure and the PathfinderFx, using the default settings.

    ```bash
    ./install.sh
    ```

3. Add your Azure account to the `Key Vault Administrator` role on the Key Vault that was created during the installation. This can be done by navigating to the Key Vault in the Azure portal keyvaultName + `-kv`, selecting `Access control (IAM)`, Add a role assignment by selecting the `Key Vault Administrator` role from the list and selecting `Next`.
   - In the Assign access to, select `User, group, or service principal` and then `+ Select members`, find your Azure account from the list and `Select`.
   - Select the `Review + assign` button and then `Review + assign`.
4. Update the `PfxConfigTemplate.json` file with the necessary configuration settings, adding and user accounts with API permissions.
5. *Optionally* minify the `PfxConfigTemplate.json` file, which removes whitespace and is not required
6. Using the Azure portal, create a new Secret called `pfx-config` in the Key Vault, then copy the contents of the `PfxConfigTemplate.json` file and paste it into an Azure Key Vault secret.
    - Navigate to the Key Vault in the Azure portal keyVaultName, select `Secrets` then `+ Generate/Import`, enter `pfx-config` for the name and paste the contents of the `PfxConfigTemplate.json` file into the `Value` field and select `Create`.
7. Generate an encryption and signing certificate to be used by the PathfinderFx.
   - For this step, you will need to have [dotnet 8](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your local machine.
   - From the console or terminal, in the [certGenerator folder](./certGenerator/) run the following command to generate the certificate, replacing `<password>` with the password you would like to use for the certificate:

      ```bash
      dotnet CertGenerator.dll <password> 
      ```

   - This will generate the `pfx-encryption-certificate.pfx` and `pfx-signing-certificate.pfx` files in the `certGenerator` folder. These certificates will need to be uploaded to the Key Vault
   - From the same console or terminal in the [certGenerator folder](./certGenerator/) run the following command to upload the certificates to the Key Vault, replacing `<keyVaultName>` and `<password>` with your key vault name and the password you used to generate the certificates:

    ```bash
        az keyvault certificate import --vault-name <keyVaultName> --name pfx-encryption-certificate --file pfx-encryption-certificate.pfx --password <password>
        az keyvault certificate import --vault-name <keyVaultName> --name pfx-signing-certificate --file pfx-signing-certificate.pfx --password <password>
    ```

## Configuration

The PathfinderFx will need permissions to access to the Key Vault. PathfinderFx is configured to use a Managed Identity, which is created during the installation process. The Managed Identity will need to be given the necessary permissions to access Key Vault by role assignment.

- The Managed Identity will need to be in the `Key Vault Certificates User` and `Key Vault Secret User` roles on the Key Vault.
  - This can be done by navigating to the Key Vault in the Azure portal *keyVaultName*, selecting `Access control (IAM)`, Add a role assignment by selecting the `Key Vault Certificates User` and `Key Vault Secret User` roles *performing the same actions as the storage role above*, e.g., `Managed Identity`, `+ Select members`, `App Service` and PathfinderFx App Service.

## Testing

You will want to restart the PathfinderFx to pick up the new configuration. This can be done by navigating to the Beacon's App Service in the Azure portal and selecting `Restart` from the top menu.

Once the host has restarted, you can test the connectivity to the other member organizations' Beacons using the [Testing Instructions](../../docs/testing-instructions.md)
