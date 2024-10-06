using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class OllamaHelper
{
    private readonly string _url;
    private readonly string _model;
    private readonly HttpClient _httpClient;

    public OllamaHelper(string url, string model, HttpClient httpClient)
    {
        _url = url;
        _model = model;
        _httpClient = httpClient;
    }

    public async Task<string> GetSummaryAsync(string content, int numberOfSentences)
    {   
        var payload = new OllamaRequest
        {
            Model = _model,
            Prompt = Constants.GetSummaryPrompt(numberOfSentences, content)
        };
        
        var json = JsonConvert.SerializeObject(payload);
        var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_url, requestContent);
        
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            return DeserializeResponses(responseData);
        }

        return string.Empty;
    }

    private string DeserializeResponses(string responseData)
    {
        var responses = string.Join("", Helpers.GetResponseLines(responseData)
            .Select(line => JsonConvert.DeserializeObject<OllamaResponse>(line)?.Response)
            .Where(response => response != null));
        
        return responses;
    }
}
