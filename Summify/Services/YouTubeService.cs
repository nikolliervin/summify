public class YouTubeService : IYouTubeService
{   
    private string _url;
    private readonly string? _key;

    private readonly HttpClient _httpClient;
    public YouTubeService(IConfiguration configuration)
    {
        _key = configuration["Keys:API_KEY"];
        _httpClient = new HttpClient();
    }
    public async Task<string> GetVideoText(string videoUrl)
    {   
        _url = $"https://www.googleapis.com/youtube/v3/search?key={_key}&part=snippet&q={Uri.EscapeDataString(videoUrl)}";

        var response = await _httpClient.GetStringAsync(_url);

        if(response != null)
            return response.ToString();
        else
            return string.Empty;
    }
}