public interface IYouTubeService : ISummarizeService
{
    Task<string> Summarize(SummarizeRequest request);
}