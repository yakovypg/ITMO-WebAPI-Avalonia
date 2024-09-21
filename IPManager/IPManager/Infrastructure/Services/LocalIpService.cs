using System.Net;
using System.Net.Sockets;

namespace IPManager.Infrastructure.Services;

public static class LocalIpService
{
    public static string GetLocalIPAddress()
    {
        try
        {
            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

            foreach (IPAddress ip in hostEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
        }
        catch { }

        return "127.0.0.1";
    }
}
