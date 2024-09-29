using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using YoutubeTranscriptApi;

public class YouTubeService : IYouTubeService
{   

    public string GetVideoText(SummarizeRequest request)
    {   
        var videoId = Helpers.GetVideoId(request.VideoUrl!);
        var youTubeTranscriptApi = new YouTubeTranscriptApi();
        
        var transcriptItems = youTubeTranscriptApi.GetTranscript(videoId);
        
        var combinedText = string.Join(" ", transcriptItems.Select(item => item.Text));

        return combinedText;
    }
}
