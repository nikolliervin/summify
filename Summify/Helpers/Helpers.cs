using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using YoutubeTranscriptApi;
using System.Text;
using iText;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
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

    public static string ExtractText(string base64Pdf)
    {   
         byte[] pdfBytes = Convert.FromBase64String(base64Pdf);
  
         using (var memoryStream = new MemoryStream(pdfBytes))
         {
             using (var pdfReader = new iText.Kernel.Pdf.PdfReader(memoryStream))
             {
                 using (var pdfDocument = new PdfDocument(pdfReader))
                 {
                     StringBuilder text = new StringBuilder();
              
                     for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                     {
                         string pageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i));
                         text.AppendLine(pageText);
                     
                     }
                     return text.ToString();

                 }
            }

        }
    }   

}