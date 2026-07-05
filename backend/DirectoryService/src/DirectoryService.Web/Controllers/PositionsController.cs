using DirectoryService.Contracts.WebApi.Positions;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PositionDto>> Create([FromBody] CreatePositionRequest request)
    {
        var id = Guid.NewGuid();

        return Created(
            new Uri($"/api/positions/{id}", UriKind.Relative),
            new PositionDto(id, "Programmer")
        );
    }

    [HttpGet("{positionId:guid}")]
    public async Task<ActionResult<PositionDto>> Get([FromRoute] Guid positionId)
    {
        return Ok(new PositionDto(positionId, "Programmer"));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PositionDto>>> GetAll()
    {
        return new List<PositionDto>();
    }

    [HttpPut("{positionId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid positionId, [FromBody] UpdatePositionRequest request)
    {
        return NoContent();
    }

    [HttpDelete("{positionId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid positionId)
    {
        return NoContent();
    }
}
