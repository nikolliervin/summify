public interface ISummarizerFactory
{
    ISummarizeService GetSummarizer(string type);
}