using Hotels.Contracts.Requests;
using Hotels.Data.Models;

namespace Hotels.Api.Mapping
{
    public static class ContractMapping
    {
        public static HotelDto MapToHotel(this CreateHotelRequest request)
        {
            return new HotelDto
            {
                Id = Guid.NewGuid(), 
                Name = request.Name,
                Category = request.Category,
                IncludesTransfers = request.IncludesTransfers,
            };
        }

        public static HotelDto MapToHotel(this UpdateHotelRequest request, Guid hotelId)
        {
            return new HotelDto
            {
                Id = hotelId,
                Name = request.Name,
                Category = request.Category,
                IncludesTransfers = request.IncludesTransfers,
            };
        }

        public static HotelResponse MapToResponse(this HotelDto hotel)
        {
            return new HotelResponse
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Category = hotel.Category,
                IncludesTransfers = hotel.IncludesTransfers,
                Rooms = hotel.Rooms,
            };
        }

        public static HotelsResponse MapToResponse(this IEnumerable<HotelDto> hotels)
        {
            return new HotelsResponse
            {
                Items = hotels.Select(MapToResponse)
            };
        }
    }
}
