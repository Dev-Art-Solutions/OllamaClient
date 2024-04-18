namespace OllamaClient.Extensions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

using Configuration;

[ExcludeFromCodeCoverage]
public static class OllamaClientServiceCollectionExtensions
{
    public const string DEFAULT_ENDPOINT_OLLAMA = "http://localhost:11434/";

    public static IServiceCollection AddOllamaClient(this IServiceCollection services)
    {
        return AddOllamaClient(services, (x) =>
        {
            x.OllamaEndpoint = DEFAULT_ENDPOINT_OLLAMA;
        });
    }

    public static IServiceCollection AddOllamaClient(this IServiceCollection services, Action<OllamaConfiguration> configurationAction)
    {
        var configuration = new OllamaConfiguration();
        configurationAction(configuration);
        services
            .AddTransient<IClient, Client>()
            .AddSingleton<IStatefulConversationOllamaService, StatefulConversationOllamaService>()
            .AddTransient((s) => configuration)
            .AddHttpClient(nameof(Client));

        return services;
    }
}
