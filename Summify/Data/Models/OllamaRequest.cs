using System.Text.Json;
using Newtonsoft.Json;
public class OllamaRequest{

    [JsonProperty("model")]
    public string? Model { get; set; }

    [JsonProperty("prompt")]
    public string? Prompt { get; set; }
}