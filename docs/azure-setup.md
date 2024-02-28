# Setting up the PathfinderFx Host in Azure

## Prerequisites

Each organization will need to have an Azure administrator that has the permissions needed to be able to deploy the Massiv+ Beacon.

## Overview

Each organization has a subfolder in the [azure](./) folder. The subfolder contains:

- A `install.sh` script that is used to install the Massiv+ Beacon, using the default organizational settings. If you would like to customize the installation, you can modify the `install.sh` script in your organization's subfolder.
- A `MpfConfig.json` file that contains the default beacon configuration, including the other member organization's access accounts and hosts. This file is modified locally, to be updated from time to time when configuration changes are made, i.e., a new member host is added. The contents of this file are then copied and pasted into an Azure Key Vault secret, which is then used by the Massiv+ Beacon for its configuration.

The install script will automate most of the installation process, including the creation of the Azure resources, the installation of the Massiv+ Beacon, but additional configuration is required to complete the installation.

## Installation Steps

The installation process is as follows:

1. Update the `install.sh` with the `subscriptionId` and `location` (set to`westus2`, but probably want `westeurope`) for the Azure subscription that the Massiv+ Beacon will be installed in. This can be found in the Azure portal by navigating to the subscription and copying the Subscription ID.
2. Run the `install.sh` script from your `Organization` folder to install the core infrastructure and the Massiv+ Beacon, using the default organizational settings. Defaults can be modified, but if so, ensure that the name of the storage account created during the installation is the same name used in the `MpfConfig.json` file.

    ```bash
    ./install.sh
    ```

3. Add your Azure account to the `Key Vault Administrator` role on the Key Vault that was created during the installation. This can be done by navigating to the Key Vault in the Azure portal `Massiv`organizationname + `-kv`, selecting `Access control (IAM)`, Add a role assignment by selecting the `Key Vault Administrator` role from the list and selecting `Next`.
   - In the Assign access to, select `User, group, or service principal` and then `+ Select members`, find your Azure account from the list and `Select`.
   - Select the `Review + assign` button and then `Review + assign`.
4. Update the `MpfConfig.json` file with the necessary configuration settings:
   - `StorageAccountKey` - The key for the storage account created during the installation will need to be copied and pasted into the `MpfConfig.json` file. Using the Azure portal, navigate to your `massiv`+ organizationname + `storage` storage account, select `Access keys` then `Show` and `Copy` the `key1` value and then paste it into the `MpfConfig.json` file, in the `StorageAccountKey` field under the `DataLakeConfig` section.
5. *Optionally* minify the `MpfConfig.json` file, which removes whitespace and is not required
6. Using the Azure portal, create a new Secret called `MpfConfig` in the Key Vault, then copy the contents of the `MpfConfig.json` file and paste it into an Azure Key Vault secret.
    - Navigate to the Key Vault in the Azure portal `Massiv`organizationname + `-kv`, select `Secrets` then `+ Generate/Import`, enter `MpfConfig` for the name and paste the contents of the `MpfConfig.json` file into the `Value` field and select `Create`.
7. Generate an encryption and signing certificate to be used by the Massiv+ Beacon.
   - For this step, you will need to have [dotnet 8](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your local machine.
   - From the console or terminal, in the [certGenerator folder](./certGenerator/) run the following command to generate the certificate, replacing `<password>` with the password you would like to use for the certificate:

      ```bash
      dotnet CertGenerator.dll <password> 
      ```

   - This will generate the `massiv-encryption-certificate.pfx` and `massiv-signing-certificate.pfx` files in the `certGenerator` folder. These certificates will need to be uploaded to the Key Vault
   - From the same console or terminal in the [certGenerator folder](./certGenerator/) run the following command to upload the certificates to the Key Vault, replacing `<organizationname>` and `<password>` with your organization name the password you used to generate the certificates:

    ```bash
        az keyvault certificate import --vault-name Massiv<organizationname>-kv --name massiv-encryption-certificate --file massiv-encryption-certificate.pfx --password <password>
        az keyvault certificate import --vault-name Massiv<organizationname>-kv --name massiv-signing-certificate --file massiv-signing-certificate.pfx --password <password>
    ```

## Configuration

The Massiv+ Beacon will need permissions to access both the storage account and the Key Vault. The beacon is configured to use a Managed Identity, which is created during the installation process. The Managed Identity will need to be given the necessary permissions to access the storage account and the Key Vault by role assignment.

- The Managed Identity will need to be in the `Storage Blob Data Contributor` role on the storage account.
  - This can be done by navigating to the storage account in the Azure portal `massiv`+ organizationname + `storage`, selecting `Access control (IAM)`, Add a role assignment by selecting the `Storage Blob Data Contributor` role from the list and selecting `Next`.
  - In the Assign access to, select `Managed Identity` and then select the `+ Select members` link. On the right side panel, under `Managed identity`, select `App Service`, then select the Massiv+ Beacon App Service (*OrgName*-Beacon) from the list and `Select`.
  - Select the `Review + assign` button and then `Review + assign`.
- The Managed Identity will need to be in the `Key Vault Certificates User` and `Key Vault Secret User` roles on the Key Vault.
  - This can be done by navigating to the Key Vault in the Azure portal `Massiv`organizationname + `-kv`, selecting `Access control (IAM)`, Add a role assignment by selecting the `Key Vault Certificates User` and `Key Vault Secret User` roles *performing the same actions as the storage role above*, e.g., `Managed Identity`, `+ Select members`, `App Service` and Beacon App Service.

## Testing

You will want to restart the Massiv+ Beacon to pick up the new configuration. This can be done by navigating to the Beacon's App Service in the Azure portal and selecting `Restart` from the top menu.

Once the Beacon has restarted, you can test the connectivity to the other member organizations' Beacons using the [Testing Instructions](../../docs/testing-instructions.md)

## Post-Installation

After you have completed the installation, you can share your Beacon's endpoints, `HostUrl` and `HostAuthUrl`, with the other member organizations. These endpoints are used to configure the other member organizations' Massiv+ Beacons to connect to your Beacon.

Example:

```json
            "HostUrl": "https://microsoft-beacon.azurewebsites.net/",
            "HostAuthUrl": "https://microsoft-beacon.azurewebsites.net/2/auth/token"
```

When another organization sends you their Beacon's endpoints, you will need to update your `MpfConfig.json` file with the new endpoints. The `MpfConfig.json` file is then copied and pasted into an Azure Key Vault secret, documented above. The Massiv+ Beacon will need to be restarted to pick up the new configuration.

- Using the Azure portal, navigate to your Beacon's App Service, select `Restart` from the top menu.
- Wait for the Beacon to restart and then you can test connectivity to the other member organizations' Beacons using the [Testing Instructions](../../docs/testing-instructions.md)
