namespace PathfinderFx.Integration.Model;

public interface IDataverseConfig
{
    public string? Url { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    
    public string? ConnectionString { get; set; }
}