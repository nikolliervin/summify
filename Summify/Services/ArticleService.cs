using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;

public class ArticleService : IArticleService
{   
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;
    public ArticleService(IOptions<AppSettings> appSettings)
    {
        _httpClient = new HttpClient();
        _appSettings = appSettings.Value;
    }
    public async Task<string> Summarize(SummarizeRequest summarizeRequest)
    {   
        summarizeRequest.Content = await _httpClient.GetAsync(summarizeRequest.Content).Result.Content.ReadAsStringAsync();
        summarizeRequest.Content = Helpers.ExtractArticleText(summarizeRequest.Content);
        var ollamaHelper = new OllamaHelper(_appSettings.OllamaAPI, _appSettings.OllamaModel, _httpClient);
        var summary = await ollamaHelper.GetSummaryAsync(summarizeRequest.Content, summarizeRequest.NumberOfSentences, Constants.GetArticleSummaryBonusPrompt());
        return summary;
    }
}
