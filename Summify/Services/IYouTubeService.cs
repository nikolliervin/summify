public interface IYouTubeService
{
    Task<string> GetVideoText(string videoUrl);
}