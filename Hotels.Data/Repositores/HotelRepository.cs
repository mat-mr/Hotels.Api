using Hotels.Data.Models;

namespace Hotels.Data.Repositores
{
    public class HotelRepository : IHotelRepository
    {
        private readonly List<HotelDto> _hotels = new();

        public HotelRepository() { }

        public async Task<HotelDto?> GetByIdAsync(Guid id, CancellationToken token)
        {
            var hotel = _hotels.FirstOrDefault(h => h.Id == id);
            return hotel;
        }

        public async Task<IEnumerable<HotelDto>> GetAllAsync(CancellationToken token)
        {
            return _hotels;
        }

        public async Task<bool> CreateAsync(HotelDto hotel, CancellationToken token)
        {
            _hotels.Add(hotel);
            return true;
        }

        public async Task<bool> UpdateAsync(HotelDto hotel, CancellationToken token)
        {
            var index = _hotels.FindIndex(h => h.Id == hotel.Id);
            if (index == -1)
            {
                return false;
            }

            _hotels[index] = hotel;

            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token)
        {
            var result = _hotels.RemoveAll(h => h.Id == id);
            return result > 0;
        }
    }
}
