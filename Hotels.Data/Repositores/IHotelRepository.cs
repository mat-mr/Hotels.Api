using Hotels.Data.Models;

namespace Hotels.Data.Repositores
{
    public interface IHotelRepository
    {
        Task<HotelDto?> GetByIdAsync(Guid id, CancellationToken token);

        Task<HotelDto?> GetBySlugAsync(string slug, CancellationToken token);

        Task<IEnumerable<HotelDto>> GetAllAsync(CancellationToken token);

        Task<bool> CreateAsync(HotelDto hotel, CancellationToken token);

        Task<bool> UpdateAsync(HotelDto hotel, CancellationToken token);

        Task<bool> DeleteByIdAsync(Guid id, CancellationToken token);

        Task<bool> ExistsByIdAsync(Guid id, CancellationToken token);
    }
}
