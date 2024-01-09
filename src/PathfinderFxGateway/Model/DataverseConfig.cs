namespace PathfinderFxGateway.Model;

public class DataverseConfig: IDataverseConfig
{
    public string? Url { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }

    public string? ConnectionString
    {
        get
        {
            var connectionString = $@"AuthType = OAuth;Url = {Url}; UserName = {UserName}; Password = {Password}; AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;LoginPrompt=Auto;RequireNewInstance = True";
            return connectionString;
        }
        set { }
    }
}