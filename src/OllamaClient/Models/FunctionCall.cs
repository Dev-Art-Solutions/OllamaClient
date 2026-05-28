namespace OllamaClient.Models;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// The function the model invoked inside a <see cref="ToolCall"/>.
/// </summary>
public class FunctionCall
{
    /// <summary>
    /// name: name of the function to call
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// arguments: key/value arguments to pass to the function
    /// </summary>
    [JsonPropertyName("arguments")]
    public JsonElement Arguments { get; set; }
}
