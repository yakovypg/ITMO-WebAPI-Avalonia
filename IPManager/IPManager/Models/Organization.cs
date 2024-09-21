using System;

namespace IPManager.Models
{
    public class Organization
    {
        public Organization(string name, string ip)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            ArgumentNullException.ThrowIfNull(ip, nameof(ip));

            Name = name;
            Ip = ip;
        }

        public string Name { get; init; }
        public string Ip { get; init; }

        public override string ToString()
        {
            return Name;
        }
    }
}
