using Newtonsoft.Json;
using System.Text;
public class YoutubeService : IYouTubeService
{   
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    public YoutubeService(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
    }
    public async Task<string> Summarize(SummarizeRequest summarizeRequest)
    {   
        
        var httpClientTimeout = _configuration["HttpClientTimeout"];
        var url = _configuration["OllamaAPI"];
        var model = _configuration["OllamaModel"];
        var videoId = Helpers.GetVideoId(summarizeRequest.Content);
        var videoTranscript = Helpers.GetVideoText(videoId);
        _httpClient.Timeout = TimeSpan.FromMinutes(Convert.ToInt32(3000));

        var payload = new OllamaRequest{
             Model = model,
             Prompt = Constants.GetSummaryPrompt(10, summarizeRequest.Content)
        };
        var json = JsonConvert.SerializeObject(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        if(response.IsSuccessStatusCode){
            var responseData = response.Content.ReadAsStringAsync().Result;
            var responses = string.Join("", Helpers.GetResponseLines(responseData)
                    .Select(line => JsonConvert.DeserializeObject<OllamaResponse>(line)?.Response)
                    .Where(response => response != null));

            return responses;
          }
        
        else{
            return string.Empty;
        }
        
        
    }
}