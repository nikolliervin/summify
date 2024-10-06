using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/summarize")]
    public class SummarizeController : ControllerBase
    {   
       private readonly ISummarizerFactory _summarizerFactory;

    public SummarizeController(ISummarizerFactory summarizerFactory)
    {
        _summarizerFactory = summarizerFactory;
    }

    [HttpPost]
    public async Task<IActionResult> Summarize([FromBody] SummarizeRequest summarizeRequest)
    {
        var summarizer = _summarizerFactory.GetSummarizer(summarizeRequest.Type);
        if (summarizer == null)
        {
            return BadRequest("Summarizer not found.");
        }

        var content = await summarizer.Summarize(summarizeRequest);
        
        return Ok(content);
    }

    }