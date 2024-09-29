  public interface ISummarizeService
    {
        Task<string> Summarize(string text, int sentences);
    }