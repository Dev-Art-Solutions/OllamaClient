namespace OllamaClient.Extensions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

using Configuration;

/// <summary>
/// Extension methods for registering OllamaClient services in an <see cref="IServiceCollection"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public static class OllamaClientServiceCollectionExtensions
{
    /// <summary>The default Ollama server base URL used when no configuration action is provided.</summary>
    public const string DEFAULT_ENDPOINT_OLLAMA = "http://localhost:11434/";

    /// <summary>
    /// Registers OllamaClient services using the default local endpoint (<c>http://localhost:11434/</c>).
    /// </summary>
    /// <param name="services">The service collection to add to.</param>
    /// <returns>The same <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddOllamaClient(this IServiceCollection services)
    {
        return AddOllamaClient(services, (x) =>
        {
            x.OllamaEndpoint = DEFAULT_ENDPOINT_OLLAMA;
        });
    }

    /// <summary>
    /// Registers OllamaClient services with a custom configuration action.
    /// </summary>
    /// <param name="services">The service collection to add to.</param>
    /// <param name="configurationAction">Delegate that configures <see cref="OllamaConfiguration"/>.</param>
    /// <returns>The same <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddOllamaClient(this IServiceCollection services, Action<OllamaConfiguration> configurationAction)
    {
        var configuration = new OllamaConfiguration();
        configurationAction(configuration);
        services
            .AddTransient<IOllamaHttpClient, OllamaHttpClient>()
            .AddSingleton<IStatefulConversationOllamaService, StatefulConversationOllamaService>()
            .AddTransient((s) => configuration)
            .AddHttpClient<OllamaHttpClient>(o => o.BaseAddress = new Uri(configuration.OllamaEndpoint));

        return services;
    }
}
