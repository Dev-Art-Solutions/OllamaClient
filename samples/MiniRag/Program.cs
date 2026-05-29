// Mini-RAG sample — embed a small knowledge base, find the most relevant chunk via cosine
// similarity, then pass it as context to the LLM.
// Run: dotnet run --project samples/MiniRag/MiniRag.csproj

using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using OllamaClient;
using OllamaClient.Extensions;
using OllamaClient.Models;

var services = new ServiceCollection()
    .AddOllamaClient()
    .BuildServiceProvider();

var client = services.GetRequiredService<IOllamaHttpClient>();
const string embedModel = "nomic-embed-text";
const string chatModel = "llama3.2";

// --- Knowledge base ---
var chunks = new[]
{
    "The Eiffel Tower is located in Paris, France, and was completed in 1889.",
    "The Great Wall of China stretches over 21,000 km and was built over many centuries.",
    "The Colosseum in Rome was completed around 80 AD and could seat up to 80,000 spectators.",
    "Machu Picchu is an Inca citadel in Peru, built in the 15th century at 2,430 m altitude.",
    "The Taj Mahal in Agra, India, was built by Mughal emperor Shah Jahan in memory of his wife."
};

// --- Embed all chunks ---
var chunkEmbeds = await client.Embed(new EmbedRequest
{
    Model = embedModel,
    Input = JsonSerializer.SerializeToElement(chunks)
}, CancellationToken.None);

// --- Embed the query ---
var query = "Where is the Eiffel Tower and when was it built?";
var queryEmbed = await client.Embed(new EmbedRequest
{
    Model = embedModel,
    Input = JsonSerializer.SerializeToElement(query)
}, CancellationToken.None);

var queryVec = queryEmbed.Embeddings[0];

// --- Cosine similarity retrieval ---
var bestIndex = chunkEmbeds.Embeddings
    .Select((vec, i) => (score: CosineSimilarity(queryVec, vec), i))
    .MaxBy(x => x.score)
    .i;

var context = chunks[bestIndex];
Console.WriteLine($"Retrieved chunk [{bestIndex}]: {context}\n");

// --- Generate answer ---
var answer = await client.SendChat(new ChatRequest
{
    Model = chatModel,
    Messages =
    [
        new Message
        {
            Role = "system",
            Content = $"Answer using only the following context:\n\n{context}"
        },
        new Message { Role = "user", Content = query }
    ]
}, CancellationToken.None);

Console.WriteLine($"Answer: {answer.Message?.Content}");

static float CosineSimilarity(List<float> a, List<float> b)
{
    float dot = 0, normA = 0, normB = 0;
    for (int i = 0; i < a.Count; i++)
    {
        dot += a[i] * b[i];
        normA += a[i] * a[i];
        normB += b[i] * b[i];
    }
    return dot / (MathF.Sqrt(normA) * MathF.Sqrt(normB));
}
