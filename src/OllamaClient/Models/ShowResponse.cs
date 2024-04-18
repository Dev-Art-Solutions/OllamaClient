using System.Text.Json.Serialization;

namespace OllamaClient.Models;

/// <summary>
/// Show Model Information
/// </summary>
public class ShowResponse
{
    /// <summary>
    /// modelfile: the contents of the Modelfile
    /// </summary>
    [JsonPropertyName("modelfile")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Modelfile { get; set; }

    /// <summary>
    /// parameters: parameters of the model
    /// </summary>
    [JsonPropertyName("parameters")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Parameters { get; set; }

    /// <summary>
    /// template: the prompt template to use (overrides what is defined in the Modelfile)
    /// </summary>
    [JsonPropertyName("template")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Template { get; set; }

    /// <summary>
    /// details: model details
    /// </summary>
    [JsonPropertyName("details")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DetailsResponse? Details { get; set; }
}
