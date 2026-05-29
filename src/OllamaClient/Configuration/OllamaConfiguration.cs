namespace OllamaClient.Configuration;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Configuration options for <see cref="OllamaClient.OllamaHttpClient"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public class OllamaConfiguration
{
    /// <summary>
    /// Base URL of the Ollama server (e.g. <c>http://localhost:11434/</c>).
    /// </summary>
    public string OllamaEndpoint { get; set; } = string.Empty;
}
