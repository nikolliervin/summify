using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/summarize")]
    public class SummarizeController : ControllerBase
    {   
        private readonly ISummarizeService _summarizeService;
        private readonly IYouTubeService _youtubeService;
        public SummarizeController(ISummarizeService summarizeService, IYouTubeService youTubeService)
        {
            _summarizeService = summarizeService;
            _youtubeService = youTubeService;
        }
        [HttpPost]
        public async Task<IActionResult> Summarize([FromBody] SummarizeRequest summarizeRequest)
        {   
            var summarizedText = string.Empty;
            if (summarizeRequest.VideoUrl == null || string.IsNullOrWhiteSpace(summarizeRequest.VideoUrl))
            {
                return BadRequest("Invalid URL.");
            }
            var videoContent = await _youtubeService.GetVideoText(summarizeRequest.VideoUrl);
            if(videoContent != null){
                //summarizedText = await _summarizeService.Summarize(videoContent);
                summarizedText = string.Empty;
            }
            else{
                return Ok("No subtitles found!");
            }
            return Ok(summarizedText);
        }

    }