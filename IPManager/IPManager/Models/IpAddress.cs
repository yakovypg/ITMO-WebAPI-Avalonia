using System;

namespace IPManager.Models
{
    public class IpAddress
    {
        public IpAddress(string ip, string details)
        {
            ArgumentNullException.ThrowIfNull(ip, nameof(ip));
            ArgumentNullException.ThrowIfNull(details, nameof(details));

            Ip = ip;
            Details = details;
        }

        public string Ip { get; init; }
        public string Details { get; init; }

        public override string ToString()
        {
            return Ip;
        }
    }
}
