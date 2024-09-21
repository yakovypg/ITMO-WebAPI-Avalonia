namespace IpManager.SharedSettings;

public static class ServerRoutes
{
    public const string FavoriteIpsRoute = "/favorites/";
    public const string OrganizationsRoute = "/organizations/";

    public static readonly string BaseUrl = $"http://{ServerSettings.Host}:{ServerSettings.Port}";
    public static readonly string FavoriteIpsUrl = $"{BaseUrl}{FavoriteIpsRoute}";
    public static readonly string OrganizationsUrl = $"{BaseUrl}{OrganizationsRoute}";
}
