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
            var videoContent = _youtubeService.GetVideoText(summarizeRequest);
            if(videoContent != null){
                return Ok(await _summarizeService.Summarize(videoContent, summarizeRequest.NumberOfSentences));
            }
            else{
                return BadRequest("Video has no captions!");
            }
            
        }

    }