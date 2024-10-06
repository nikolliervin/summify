using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using YoutubeTranscriptApi;
public static class Helpers{
    public static string GetVideoId(string url)
    {
        var regex = new Regex(@"(?:https?:\/\/)?(?:www\.)?(?:youtube\.com\/(?:[^\/\n\s]+\/\S+\/|(?:v|e(?:mbed)?)\/|.*[?&]v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})");
        var match = regex.Match(url);
        
        return match.Success ? match.Groups[1].Value : null;
    }
    
    public static List<string> GetResponseLines(string response){
        var lines = response
                    .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
          if (lines.Count > 0)
                    lines.RemoveAt(lines.Count - 1);
        return lines;
                

    }

    public static string GetVideoText(string videoId)
    {   
        var youTubeTranscriptApi = new YouTubeTranscriptApi();
        
        var transcriptItems = youTubeTranscriptApi.GetTranscript(videoId);
        
        var combinedText = string.Join(" ", transcriptItems.Select(item => item.Text));

        return combinedText;
    }
}