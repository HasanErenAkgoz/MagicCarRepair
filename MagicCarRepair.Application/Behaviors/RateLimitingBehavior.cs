using System.Threading.RateLimiting;
using MagicCarRepair.Application.Common;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace MagicCarRepair.Application.Behaviors;

public class RateLimitingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IMemoryCache _cache;
    private readonly RateLimiter _rateLimiter;

    public RateLimitingBehavior(IMemoryCache cache)
    {
        _cache = cache;
        _rateLimiter = new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromMinutes(15)
        });
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is ILimitedRequest limitedRequest)
        {
            var key = $"ratelimit_{limitedRequest.GetLimitKey()}";
            var attempts = await _cache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                return Task.FromResult(0);
            });

            using var lease = await _rateLimiter.AcquireAsync(1, cancellationToken);
            if (!lease.IsAcquired)
            {
                throw new Exception("Too many requests. Please try again later.");
            }

            _cache.Set(key, attempts + 1, TimeSpan.FromMinutes(15));
        }

        return await next();
    }
}