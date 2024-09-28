using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using YoutubeTranscriptApi;

public class YouTubeService : IYouTubeService
{   
    private readonly HttpClient _httpClient;

    public YouTubeService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
    }

    public string GetVideoText(SummarizeRequest request)
    {   
        var videoId = Helpers.GetVideoId(request.VideoUrl!);
        var youTubeTranscriptApi = new YouTubeTranscriptApi();
        
        var transcriptItems = request.Language != null 
        ? youTubeTranscriptApi.GetTranscript(videoId, request.Language) 
        : youTubeTranscriptApi.GetTranscript(videoId);
        
        var combinedText = string.Join(" ", transcriptItems.Select(item => item.Text));

        return combinedText;
    }
}
