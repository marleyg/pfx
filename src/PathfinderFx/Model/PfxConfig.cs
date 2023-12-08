
namespace PathfinderFx.Model;

public interface IPfxConfig
{
    List<ConformanceAccount> ConformanceAccounts { get; set; }
    public string EncryptionCertificateThumbprint { get; set; }
    public string SigningCertificateThumbprint { get; set; }
}

public class PfxConfig : IPfxConfig
{
    public List<ConformanceAccount> ConformanceAccounts { get; set; } = new();
    public string EncryptionCertificateThumbprint { get; set; } = "";
    public string SigningCertificateThumbprint { get; set; } = "";
}

public class ConformanceAccount
{
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
    public string DisplayName { get; set; } = "";
}