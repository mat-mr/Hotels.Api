using Hotels.Api.Mapping;
using Hotels.Contracts.Requests;
using Hotels.Data.Repositores;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers
{
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelsController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpPost(ApiEndpoints.Hotels.Create)]
        public async Task<IActionResult> Create([FromBody] CreateHotelRequest request, CancellationToken token)
        {
            var hotel = request.MapToHotel();

            var created = await _hotelRepository.CreateAsync(hotel, token);  

            return CreatedAtAction(nameof(Get), new { id = hotel.Id }, hotel);
        }

        [HttpPut(ApiEndpoints.Hotels.Update)]
        public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody] UpdateHotelRequest request, CancellationToken token)
        {
            var hotel = request.MapToHotel(id);

            var updated = await _hotelRepository.UpdateAsync(hotel, token);
            if (!updated)
            {
                return NotFound();
            }

            return Ok(hotel.MapToResponse());
        }

        [HttpDelete(ApiEndpoints.Hotels.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
        {
            var deleted = await _hotelRepository.DeleteByIdAsync(id, token);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet(ApiEndpoints.Hotels.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id, token);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel.MapToResponse());
        }

        [HttpGet(ApiEndpoints.Hotels.GetAll)]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var hotels = await _hotelRepository.GetAllAsync(token);
            
            return Ok(hotels.MapToResponse());
        }
    }
}
