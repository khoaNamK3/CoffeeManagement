using System.Text.Json.Serialization;

namespace CoffeeManagement.MetaData
{
    public class PagingResponse<T>
    {
        [JsonPropertyName("items")]
        public IEnumerable<T> Items { get; set; }

        [JsonPropertyName("meta")]
        public PaginationMeta Meta { get; set; }
    }
}
