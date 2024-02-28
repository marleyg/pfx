using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using Azure.Security.KeyVault.Secrets;
using Newtonsoft.Json;
using PathfinderFx.Model;

namespace PathfinderFx.Providers;

public static class AksConfigurationProvider
{
    
    private static readonly string AksUri = Environment.GetEnvironmentVariable("AKS_URI") ??
                                            throw new InvalidOperationException("Environment variable AKS_URI is not set");

    /// <summary>
    /// Loads the configuration data from the Azure Key Vault.
    /// </summary>
    /// <exception cref="Exception"></exception>'
    static AksConfigurationProvider()
    {
        var secretName = Environment.GetEnvironmentVariable("PFX_CONFIG_SECRET_NAME");
        if (string.IsNullOrEmpty(AksUri) || string.IsNullOrEmpty(secretName))
            throw new Exception("AKS_URI or MPF_CONFIG_SECRET_NAME is not set in the environment");
        var client = new SecretClient(vaultUri: new Uri(AksUri), credential: new DefaultAzureCredential());
        var secret = client.GetSecret(secretName);
        try
        {
            PfxConfig = JsonConvert.DeserializeObject<PfxConfig>(secret.Value.Value);
        }
        catch (Exception e)
        {
            throw new Exception("Error deserializing the secret for MpfConfig", e);
        }
    }
    
    /// <summary>
    /// Massiv Plus Framework configuration from Azure Key Vault.
    /// </summary>
    public static IPfxConfig? PfxConfig { get; private set; }
    
    internal static X509Certificate2 GetEncryptionCertFromAks()
    {
        // Create a new certificate client using the default credential from Azure.Identity using environment variables previously set,
        // including AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, and AZURE_TENANT_ID.
        var etp = Environment.GetEnvironmentVariable("ENCRYPTION_CERTIFICATE_NAME") ??
                  throw new InvalidOperationException("Environment variable ENCRYPTION_CERTIFICATE_NAME is not set");
        return GetCertFromAks(etp);
    }
    
    internal static X509Certificate2 GetSigningCertFromAks()
    {
        // Create a new certificate client using the default credential from Azure.Identity using environment variables previously set,
        // including AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, and AZURE_TENANT_ID.
        var stp = Environment.GetEnvironmentVariable("SIGNING_CERTIFICATE_NAME") ??
                  throw new InvalidOperationException("Environment variable SIGNING_CERTIFICATE_NAME is not set");
        return GetCertFromAks(stp);
    }
    private static X509Certificate2 GetCertFromAks(string certName)
    {
        var client = new CertificateClient(vaultUri: new Uri(AksUri), credential: new DefaultAzureCredential());
        var certificate = client.DownloadCertificate(certName);
        return certificate;
    }
}