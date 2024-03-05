using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CertGenerator;

public class Encryption
{
    public void CreateEncryptionCert(string password)
    {
        using var algorithm = RSA.Create(keySizeInBits: 2048);

        var subject = new X500DistinguishedName("CN=PathfinderFx Encryption Certificate");
        var request = new CertificateRequest(
            subject, algorithm, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        request.CertificateExtensions.Add(
            new X509KeyUsageExtension(X509KeyUsageFlags.KeyEncipherment, critical: true));

        var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2));

        File.WriteAllBytes("pfx-encryption-certificate.pfx",
            certificate.Export(X509ContentType.Pfx,  password));
    }
}