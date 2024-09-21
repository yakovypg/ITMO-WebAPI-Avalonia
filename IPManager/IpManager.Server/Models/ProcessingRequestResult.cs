using System.Net;

namespace IpManager.Server.Models;

public record ProcessingRequestResult(string ResponseContent, HttpStatusCode StatusCode);
