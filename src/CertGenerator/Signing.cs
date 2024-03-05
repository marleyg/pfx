using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CertGenerator;

public class Signing
{
    public void CreateSigningCert(string password)
    {
        using var algorithm = RSA.Create(keySizeInBits: 2048);

        var subject = new X500DistinguishedName("CN=PathfinderFx Signing Certificate");
        var request = new CertificateRequest(
            subject, algorithm, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        request.CertificateExtensions.Add(
            new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, critical: true));

        var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2));

        File.WriteAllBytes("pfx-signing-certificate.pfx",
            certificate.Export(X509ContentType.Pfx, password));
    }
}