public interface ITextService : ISummarizeService
{
    Task<string> Summarize(SummarizeRequest request);
}