  public interface ISummarizeService
    {
        Task<string> Summarize(SummarizeRequest summarizeRequest);
    }