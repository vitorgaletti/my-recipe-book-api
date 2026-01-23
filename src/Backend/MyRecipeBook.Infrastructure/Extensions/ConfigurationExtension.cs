using Microsoft.Extensions.Configuration;

namespace MyRecepiBook.Infrastructure.Extensions;

public static class ConfigurationExtension
{
    public static bool IsUniTestEnviroment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }
    public static string ConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("Connection")!;
    }
    
}