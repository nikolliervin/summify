using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class SummarizeController : ControllerBase
    {   
        private readonly SummarizeService _summarizeService;
        private readonly YouTubeService _youtubeService;
        public SummarizeController(SummarizeService summarizeService, YouTubeService youTubeService)
        {
            _summarizeService = summarizeService;
            _youtubeService = _youtubeService;
        }
        [HttpPost]
        public IActionResult Summarize([FromBody] string videURL)
        {
            if (videURL == null || string.IsNullOrWhiteSpace(videURL))
            {
                return BadRequest("Invalid URL.");
            }
            return Ok(string.Empty);
        }

    }