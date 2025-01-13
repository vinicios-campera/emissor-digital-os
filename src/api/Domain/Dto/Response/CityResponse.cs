using System.Text.Json.Serialization;

namespace OrderService.Domain.Dto.Response
{
    public class CityResponse
    {
        [JsonPropertyName("cep")]
        public string? Cep { get; set; }

        [JsonPropertyName("uf")]
        public string? State { get; set; }

        [JsonPropertyName("localidade")]
        public string? City { get; set; }
    }
}