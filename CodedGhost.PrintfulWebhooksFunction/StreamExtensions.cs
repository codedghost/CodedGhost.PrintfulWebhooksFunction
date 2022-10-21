using Newtonsoft.Json;

namespace CodedGhost.PrintfulWebhooksFunction;

// Should think about pulling this out to a small library package.
public static class StreamExtensions
{
    public static async Task<T?> DeserializeFromStream<T>(this Stream stream)
    {
        var content = await new StreamReader(stream).ReadToEndAsync();

        return JsonConvert.DeserializeObject<T>(content);
    }
}