using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

public class PdfService : IPdfService
{   
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;
    public PdfService(IOptions<AppSettings> appSettings)
    {
        _httpClient = new HttpClient();
        _appSettings = appSettings.Value;
    }
    public async Task<string> Summarize(SummarizeRequest summarizeRequest)
    {
       summarizeRequest.Content = Helpers.ExtractText(summarizeRequest.Content);
       _httpClient.Timeout = TimeSpan.FromMinutes(Convert.ToInt32(_appSettings.HttpClientTimeout));
   
       var ollamaHelper = new OllamaHelper(_appSettings.OllamaAPI, _appSettings.OllamaModel, _httpClient);
       var summary = await ollamaHelper.GetSummaryAsync(summarizeRequest.Content, 10);
       
       return summary;
    }
}
