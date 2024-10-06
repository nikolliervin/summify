public interface IPdfService: ISummarizeService
{
    Task<string> Summarize(SummarizeRequest request);
}
