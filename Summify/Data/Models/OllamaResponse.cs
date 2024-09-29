using Newtonsoft.Json;

public class OllamaResponse
{   
    [JsonProperty("model")]
    public string? Model { get; set; }

    [JsonProperty("created_at")]
    public string? CreatedAt { get; set; }

    [JsonProperty("response")]
    public string? Response { get; set; }

    [JsonProperty("done")]
    public bool Done { get; set; }
}