public class SummarizerFactory : ISummarizerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public SummarizerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISummarizeService GetSummarizer(string type)
    {
        return type switch
        {
            "youtube" => _serviceProvider.GetService<IYouTubeService>()!,
            "pdf" => _serviceProvider.GetService<IPdfService>()!,
        _   => throw new ArgumentException("Invalid summarization type")
        };
    }

}