namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// The message object has the following fields
/// </summary>
public class Message
{
    /// <summary>
    /// role: the role of the message, either system, user or assistant
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
}
