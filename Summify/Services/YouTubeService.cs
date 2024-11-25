using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
public class YoutubeService : IYouTubeService
{   
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;
    public YoutubeService(IOptions<AppSettings> appSettings)
    {
        _httpClient = new HttpClient();
        _appSettings = appSettings.Value;
    }
    public async Task<string> Summarize(SummarizeRequest summarizeRequest)
    {
       var videoId = Helpers.GetVideoId(summarizeRequest.Content);
       summarizeRequest.Content = Helpers.GetVideoText(videoId);
       
       _httpClient.Timeout = TimeSpan.FromMinutes(Convert.ToInt32(_appSettings.HttpClientTimeout));
   
       var ollamaHelper = new OllamaHelper(_appSettings.OllamaAPI, summarizeRequest.Model, _httpClient);
       var summary = await ollamaHelper.GetSummaryAsync(summarizeRequest.Content, summarizeRequest.NumberOfSentences, summarizeRequest.Model);
       
       return summary;
    }
}