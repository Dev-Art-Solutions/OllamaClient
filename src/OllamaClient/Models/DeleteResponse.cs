namespace OllamaClient.Models;

/// <summary>
/// Delete Response
/// </summary>
public class DeleteResponse
{
    /// <summary>
    /// Returns a 200 OK if successful, 404 Not Found if the model to be deleted doesn't exist.
    /// </summary>
    public bool IsSuccessful { get; set; }
}
