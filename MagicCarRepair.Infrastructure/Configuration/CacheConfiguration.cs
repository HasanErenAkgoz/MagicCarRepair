namespace MagicCarRepair.Infrastructure.Configuration;

public interface ICacheConfiguration
{
    string Strategy { get; set; }
    RedisConfiguration Redis { get; set; }
    MemoryConfiguration Memory { get; set; }
}

public class CacheConfiguration : ICacheConfiguration
{
    public string Strategy { get; set; }
    public RedisConfiguration Redis { get; set; }
    public MemoryConfiguration Memory { get; set; }
}

public class RedisConfiguration
{
    public string ConnectionString { get; set; }
    public string InstanceName { get; set; }
}

public class MemoryConfiguration
{
    public int SlidingExpirationMinutes { get; set; }
    public int AbsoluteExpirationMinutes { get; set; }
} 