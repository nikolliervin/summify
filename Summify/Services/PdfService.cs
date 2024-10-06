using Microsoft.AspNetCore.Http.HttpResults;

public class PdfService : IPdfService
{   
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    public PdfService(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
    }
    public async Task<string> Summarize(SummarizeRequest summarizeRequest)
    {
       var httpClientTimeout = _configuration["HttpClientTimeout"];
       var url = _configuration["OllamaAPI"];
       var model = _configuration["OllamaModel"];
       summarizeRequest.Content = Helpers.ExtractText(summarizeRequest.Content);
       _httpClient.Timeout = TimeSpan.FromMinutes(Convert.ToInt32(httpClientTimeout));
   
       var ollamaHelper = new OllamaHelper(url, model, _httpClient);
       var summary = await ollamaHelper.GetSummaryAsync(summarizeRequest.Content, 10);
       
       return summary;
    }
}
