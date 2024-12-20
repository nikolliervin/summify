using Newtonsoft.Json;
using System.Text;
public class SummarizeService : ISummarizeService
{   
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    public SummarizeService(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
    }
    public async Task<string> Summarize(string text, int sentences)
    {   
        
        var httpClientTimeout = _configuration["HttpClientTimeout"];
        var url = _configuration["OllamaAPI"];
        var model = _configuration["OllamaModel"];

        _httpClient.Timeout = TimeSpan.FromMinutes(Convert.ToInt32(3000));

        var payload = new OllamaRequest{
             Model = model,
             Prompt = Constants.GetSummaryPrompt(10, text)
        };
        var json = JsonConvert.SerializeObject(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        if(response.IsSuccessStatusCode){
            var responseData = response.Content.ReadAsStringAsync().Result;
            var responses = string.Join("", Helpers.GetResponseLines(responseData)
                    .Select(line => JsonConvert.DeserializeObject<OllamaResponse>(line)?.Response)
                    .Where(response => response != null));

            return responses;
          }
        
        else{
            return string.Empty;
        }
        
        
    }
}