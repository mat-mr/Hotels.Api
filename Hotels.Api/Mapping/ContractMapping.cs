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
    }
}
