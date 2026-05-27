namespace OllamaClient.Extensions;

using System.Runtime.CompilerServices;
using System.Text.Json;

internal static class HttpContentNdjsonExtensions
{
    private static readonly JsonSerializerOptions _serializerOptions
        = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

    public static async IAsyncEnumerable<TValue> ReadFromNdjsonAsync<TValue>(this HttpContent content, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(content);

        string? mediaType = content.Headers.ContentType?.MediaType;

        if (mediaType is null || !mediaType.Equals("application/x-ndjson", StringComparison.OrdinalIgnoreCase))
        {
            throw new NotSupportedException();
        }

        Stream contentStream = await content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

        using (contentStream)
        {
            using StreamReader contentStreamReader = new(contentStream);
            string? line;
            while ((line = await contentStreamReader.ReadLineAsync(cancellationToken).ConfigureAwait(false)) is not null)
            {
                yield return JsonSerializer.Deserialize<TValue>(line, _serializerOptions)!;
            }
        }
    }
}
