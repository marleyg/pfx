param keyVaultUri string
param webAppName string

resource webApp 'Microsoft.Web/sites@2022-03-01' existing = {
  name: webAppName
}

resource siteconfig 'Microsoft.Web/sites/config@2023-01-01' = {
  parent: webApp
  name: 'appsettings'
  properties: {
      ASPNETCORE_ENVIRONMENT: 'Production'
      ENCRYPTION_CERTIFICATE_NAME: 'pfx-encryption-certificate'
      SIGNING_CERTIFICATE_NAME: 'pfx-signing-certificate'
      PFX_CONFIG_SECRET_NAME: 'pfx-config'
      AKV_URI: keyVaultUri
    }
  }
