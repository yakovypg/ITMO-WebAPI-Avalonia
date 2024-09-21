using System.Net;

namespace StarWars.Server.Models
{
    public record PerformRequestResult(string ResponseContent, HttpStatusCode StatusCode);
}
