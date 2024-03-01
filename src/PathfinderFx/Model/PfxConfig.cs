
namespace PathfinderFx.Model;

public interface IPfxConfig
{
    List<ConformanceAccount> ConformanceAccounts { get; set; }
}

public class PfxConfig : IPfxConfig
{
    public List<ConformanceAccount> ConformanceAccounts { get; set; } = new();
}

public class ConformanceAccount
{
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
    public string DisplayName { get; set; } = "";

    public List<string> Permissions { get; set; } = [];
}