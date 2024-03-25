
namespace PathfinderFx.Model;

public interface IPfxConfig
{
    List<PathfinderAccount> PathfinderAccounts { get; set; }
}

public class PfxConfig : IPfxConfig
{
    public List<PathfinderAccount> PathfinderAccounts { get; set; } = new();
}

public class PathfinderAccount
{
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
    public string DisplayName { get; set; } = "";

    public List<string> Permissions { get; set; } = [];
}