namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Defines a tool that the model can call during a chat.
/// </summary>
public class Tool
{
    /// <summary>
    /// type: the type of tool; currently only "function" is supported
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "function";

    /// <summary>
    /// function: the function definition
    /// </summary>
    [JsonPropertyName("function")]
    public FunctionDefinition Function { get; set; } = default!;
}
