using Hotels.Api.Mapping;
using Hotels.Contracts.Requests;
using Hotels.Data.Repositores;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelsController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpPost("hotels")]
        public async Task<IActionResult> Create([FromBody] CreateHotelRequest request, CancellationToken token)
        {
            var hotel = request.MapToHotel();

            var result = await _hotelRepository.CreateAsync(hotel, token);  

            return Ok(hotel);
        }
    }
}
