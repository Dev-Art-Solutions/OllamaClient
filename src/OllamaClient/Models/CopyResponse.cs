namespace OllamaClient.Models;

/// <summary>
/// Copy Response
/// </summary>
public class CopyResponse
{
    /// <summary>
    /// Returns a 200 OK if successful, or a 404 Not Found if the source model doesn't exist.
    /// </summary>
    public bool IsSuccessful { get; set; }
}
