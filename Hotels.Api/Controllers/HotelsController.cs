using Hotels.Api.Mapping;
using Hotels.Contracts.Requests;
using Hotels.Contracts.Responses;
using Hotels.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers;

[ApiController]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpPost(ApiEndpoints.Hotels.Create)]
    public async Task<IActionResult> Create([FromBody] CreateHotelRequest request, CancellationToken token)
    {
        var hotel = request.MapToHotel();
        var createResult = await _hotelService.CreateAsync(hotel, token);

        return createResult.Match<IActionResult>(
            success => CreatedAtAction(nameof(Get), new { idOrSlug = hotel.Id }, hotel),
            failure => BadRequest(failure.Errors)
            );
    }

    [HttpPut(ApiEndpoints.Hotels.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateHotelRequest request, CancellationToken token)
    {
        var hotel = request.MapToHotel(id);
        var updateResult = await _hotelService.UpdateAsync(hotel, token);
        
        return updateResult.Match<IActionResult>(
            hotel => Ok(hotel.MapToResponse()),
            notFound => NotFound(),
            failure => BadRequest(failure.Errors)
            );
    }

    [HttpDelete(ApiEndpoints.Hotels.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
    {
        var deleteResult = await _hotelService.DeleteByIdAsync(id, token);

        return deleteResult.Match<IActionResult>(
            success => Ok(),
            notFound => NotFound()
            );
    }

    [HttpGet(ApiEndpoints.Hotels.Get)]
    public async Task<IActionResult> Get([FromRoute] string idOrSlug, CancellationToken token)
    {
        var getResult = Guid.TryParse(idOrSlug, out var id)
            ? await _hotelService.GetByIdAsync(id, token)
            : await _hotelService.GetBySlugAsync(idOrSlug, token);

        return getResult.Match<IActionResult>(
            hotel => Ok(hotel.MapToResponse()),
            notFound => NotFound()
            );
    }

    [HttpGet(ApiEndpoints.Hotels.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllHotelsRequest request, CancellationToken token)
    {
        var options = request.MapToOptions();
        var hotels = await _hotelService.GetAllAsync(options, token);
        
        return Ok(hotels.MapToResponse());
    }
}
