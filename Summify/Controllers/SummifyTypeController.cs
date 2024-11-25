using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/types")]
public class SummifyTypeController : ControllerBase{
    private readonly ITypeService _typeService;
    public SummifyTypeController(ITypeService typeService)
    {
        _typeService = typeService;
    }
    [HttpGet]
    public async Task<IActionResult> Types()
    {
        return Ok(_typeService.Types());
    }

    [HttpGet("models")]
    public async Task<IActionResult> Models()
    {
        return Ok(_typeService.Models());
    }

}