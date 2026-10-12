namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// A tool call returned by the model inside a chat response message.
/// </summary>
public class ToolCall
{
    /// <summary>
    /// id: identifier of this tool call; echo it back in <see cref="Message.ToolCallId"/> on the tool result message
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    /// <summary>
    /// function: the function the model wants to invoke
    /// </summary>
    [JsonPropertyName("function")]
    public FunctionCall Function { get; set; } = default!;
}
