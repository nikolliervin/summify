using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class SummarizeController : ControllerBase
    {
        // POST: api/summarize
        [HttpPost]
        public IActionResult Summarize([FromBody] string videURL)
        {
            if (videURL == null || string.IsNullOrWhiteSpace(videURL))
            {
                return BadRequest("Invalid URL.");
            }
            return Ok("Text");
        }

    }