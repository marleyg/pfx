# Setting up the PathfinderFx Host in Azure

Installing the PathfinderFx Host in Azure involves creating the necessary Azure resources and then installing the host. This install is the minimum non-enterprise configuration, and is intended to be used for testing and development purposes. The installation process is automated using a series of scripts, which will create the necessary Azure resources and install the PathfinderFx. The installation process will require some manual configuration to complete the installation.

This code base can switch from using a local accounts database to using Azure AD B2C for authentication. This is not covered in this document, but the code base is set up to use Azure AD B2C for authentication.

## Prerequisites

You will need to have an Azure subscription and an administrator account that has the permissions needed to be able to deploy the PathfinderFx. You will also need to have the following installed on your local machine:

- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)
- [dotnet 8](https://dotnet.microsoft.com/download/dotnet/8.0)

## Overview

You will run a series of scripts to install the PathfinderFx Host:

- A `install.sh` script that is used to install the PathfinderFx, using the default settings. If you would like to customize the installation, you can modify the `install.sh` script .
- A `PfxConfigTemplate.json` file that contains the default host configuration, including the accounts you would like to have access to the API. This file is modified locally, to be updated from time to time when configuration changes are made, i.e., a new account for access is added. The contents of this file are then copied and pasted into an Azure Key Vault secret, which is then used by the PathfinderFx for its configuration.
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

**Pre-Installation Steps:** Before you begin the installation process, you will need to do the following:

1. Clone the repository to your local machine.
2. Install the [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest) on your local machine.
3. From a `console or terminal session` Build the PathfinderFx solution using the `dotnet build -c Release` command from the [PathfinderFx](../src/PathfinderFx/) folder.
4. Zip the files from the `PathfinderFx/bin/release/net8.0` folder a single zip file. This zip file will be used to deploy the PathfinderFx to Azure.

   ```bash
   zip -r PathfinderFx.zip .
   ```

5. If using Linux or MacOs, you will need to make sure the script is executable by running the following command from the [azure](../src/azure/) folder:

   ```bash
   chmod +x install.sh
   ```

The installation process is as follows:

1. From the `src/azure` directory, update the `install.sh` with the `subscriptionId`, `hostOrgName` and `location` (currently set to `westus2`, but choose a [location](https://gist.github.com/ausfestivus/04e55c7d80229069bf3bc75870630ec8) that makes sense for your organization) for the Azure subscription. These can be found in the Azure portal by navigating to the subscription and copying the Subscription ID from any resource installed in the subscription.
2. Update the `PfxConfigTemplate.json` file with the necessary configuration settings, adding and user accounts with API permissions.
3. Run the `install.sh` script from the [azure](../src/azure/) folder to install the core infrastructure and the PathfinderFx, using the default settings.

    ```bash
    ./install.sh
    ```

## Testing

You will want to restart the PathfinderFx to pick up the new configuration. This can be done by navigating to the Pathfinder App Service in the Azure portal and selecting `Restart` from the top menu. It can take a few minutes for the restart to complete.

Once the host has restarted, you can test the connectivity to your Pathfinder host using the [Testing Instructions](./testing-instructions.md)
