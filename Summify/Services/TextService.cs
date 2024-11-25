using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
public class TextService : ITextService
{   
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;
    public TextService(IOptions<AppSettings> appSettings)
    {
        _httpClient = new HttpClient();
        _appSettings = appSettings.Value;
    }
    public async Task<string> Summarize(SummarizeRequest summarizeRequest)
    {
       var ollamaHelper = new OllamaHelper(_appSettings.OllamaAPI, summarizeRequest.Model, _httpClient);
       var summary = await ollamaHelper.GetSummaryAsync(summarizeRequest.Content, summarizeRequest.NumberOfSentences, summarizeRequest.Model);
       
       return summary;
    }
}