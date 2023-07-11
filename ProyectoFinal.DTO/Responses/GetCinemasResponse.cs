using Newtonsoft.Json;

namespace ProyectoFinal.DTO.Responses
{
    public class GetCinemasResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
