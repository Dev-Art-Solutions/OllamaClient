namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// The message object has the following fields
/// </summary>
public class Message
{
    /// <summary>
    /// role: the role of the message, either system, user, assistant or tool
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; set; } = default!;

    /// <summary>
    /// content: the content of the message
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;

    /// <summary>
    /// images (optional): a list of images to include in the message (for multimodal models such as llava)
    /// </summary>
    [JsonPropertyName("images")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Images { get; set; }

    /// <summary>
    /// tool_calls (optional): tool calls the model wants to make; present when the model invokes a function
    /// </summary>
    [JsonPropertyName("tool_calls")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ToolCall>? ToolCalls { get; set; }

    /// <summary>
    /// thinking (optional): the model's reasoning trace for this message, when the request enabled <c>think</c>.
    /// Send it back on assistant messages in later turns to preserve the reasoning context.
    /// </summary>
    [JsonPropertyName("thinking")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Thinking { get; set; }

    /// <summary>
    /// tool_name (optional): on a message with role "tool", the name of the function whose result this is
    /// </summary>
    [JsonPropertyName("tool_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ToolName { get; set; }

    /// <summary>
    /// tool_call_id (optional): on a message with role "tool", the <see cref="ToolCall.Id"/> this message answers
    /// </summary>
    [JsonPropertyName("tool_call_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ToolCallId { get; set; }
}
