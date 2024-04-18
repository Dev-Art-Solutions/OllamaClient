namespace OllamaClient.Models;

/// <summary>
/// Create Model Response
/// </summary>
public class CreateModelResponse
{
    /// <summary>
    /// A stream of JSON objects. Notice that the final JSON object shows a "status": "success".
    /// </summary>
    public string Status { get; set; } = default!;
}
