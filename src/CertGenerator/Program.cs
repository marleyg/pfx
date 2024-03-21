// See https://aka.ms/new-console-template for more information

namespace CertGenerator;

public static class Program
{
    public static void Main(string[] args)
    {
        if (!string.IsNullOrEmpty(args[0]))
        {
            Encryption.CreateEncryptionCert(args[0]);
            Console.WriteLine("encryption-certificate.pfx created");

            Signing.CreateSigningCert(args[0]);
            Console.WriteLine("signing-certificate.pfx created");
        }
        else
        {
            Console.WriteLine("Please provide a password");
        }
    }
}