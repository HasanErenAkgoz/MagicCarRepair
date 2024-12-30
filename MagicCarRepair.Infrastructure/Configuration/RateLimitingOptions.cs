namespace MagicCarRepair.Infrastructure.Configuration;

public class RateLimitingOptions
{
    public bool EnableEndpointRateLimiting { get; set; }
    public bool StackBlockedRequests { get; set; }
    public string RealIpHeader { get; set; }
    public string ClientIdHeader { get; set; }
    public int HttpStatusCode { get; set; }
    public List<RateLimitRule> GeneralRules { get; set; } = new();
}

public class RateLimitRule
{
    public string Endpoint { get; set; }
    public string Period { get; set; }
    public int Limit { get; set; }
} 