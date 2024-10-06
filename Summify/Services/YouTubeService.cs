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
       summarizeRequest.Content = Helpers.GetVideoText(videoId);
       
       _httpClient.Timeout = TimeSpan.FromMinutes(Convert.ToInt32(httpClientTimeout));
   
       var ollamaHelper = new OllamaHelper(url, model, _httpClient);
       var summary = await ollamaHelper.GetSummaryAsync(summarizeRequest.Content, 10);
       
       return summary;
    }
}