public interface IArticleService : ISummarizeService
{
    Task<string> Summarize(SummarizeRequest request);
}