namespace OllamaClient.Models;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Value of the <c>think</c> request parameter. Ollama accepts either a boolean (enable/disable thinking)
/// or a named effort level (<c>"low"</c>, <c>"medium"</c>, <c>"high"</c>, <c>"max"</c>) for models that support it.
/// Converts implicitly from <see cref="bool"/> and <see cref="string"/>, so <c>Think = true</c> and <c>Think = "high"</c> both work.
/// </summary>
[JsonConverter(typeof(ThinkValueJsonConverter))]
public readonly record struct ThinkValue
{
    private ThinkValue(bool? enabled, string? level)
    {
        Enabled = enabled;
        Level = level;
    }

    /// <summary>
    /// The boolean value, when this was created from a boolean; otherwise null.
    /// </summary>
    public bool? Enabled { get; }

    /// <summary>
    /// The named effort level, when this was created from a string; otherwise null.
    /// </summary>
    public string? Level { get; }

    /// <summary>
    /// Low thinking effort.
    /// </summary>
    public static ThinkValue Low => new(null, "low");

    /// <summary>
    /// Medium thinking effort.
    /// </summary>
    public static ThinkValue Medium => new(null, "medium");

    /// <summary>
    /// High thinking effort.
    /// </summary>
    public static ThinkValue High => new(null, "high");

    /// <summary>
    /// Maximum thinking effort.
    /// </summary>
    public static ThinkValue Max => new(null, "max");

    /// <summary>
    /// Creates a boolean think value.
    /// </summary>
    /// <param name="enabled">Whether thinking is enabled</param>
    public static implicit operator ThinkValue(bool enabled) => new(enabled, null);

    /// <summary>
    /// Creates a named-level think value.
    /// </summary>
    /// <param name="level">Effort level, e.g. "low", "medium", "high" or "max"</param>
    public static implicit operator ThinkValue(string level) => new(null, level);

    /// <inheritdoc/>
    public override string ToString() => Level ?? (Enabled == true ? "true" : "false");
}

/// <summary>
/// Serializes <see cref="ThinkValue"/> as a JSON boolean or string.
/// </summary>
public sealed class ThinkValueJsonConverter : JsonConverter<ThinkValue>
{
    /// <inheritdoc/>
    public override ThinkValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.String => reader.GetString()!,
            _ => throw new JsonException($"Unexpected token {reader.TokenType} for think value; expected a boolean or a string."),
        };
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, ThinkValue value, JsonSerializerOptions options)
    {
        if (value.Level is not null)
        {
            writer.WriteStringValue(value.Level);
        }
        else
        {
            writer.WriteBooleanValue(value.Enabled ?? false);
        }
    }
}
