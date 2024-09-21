namespace HarryPotter.ServerConnection.Services;

public static class ServerConnectionService
{
    private static int Port => 7654;
    private static string Host => "localhost";

    public static string BaseUrl => $"http://{Host}:{Port}";
    public static string LikedCharactersUrl => $"{BaseUrl}/likes/";
}
