namespace OllamaClient.Exceptions;

/// <summary>
/// Thrown when the Ollama API returns a non-success HTTP response.
/// </summary>
public class OllamaException(string message) : Exception(message)
{
}
