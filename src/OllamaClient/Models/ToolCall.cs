namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// A tool call returned by the model inside a chat response message.
/// </summary>
public class ToolCall
{
    /// <summary>
    /// function: the function the model wants to invoke
    /// </summary>
    [JsonPropertyName("function")]
    public FunctionCall Function { get; set; } = default!;
}
