using Hotels.Data.Models;
using Hotels.Services.Validators;
using OneOf;
using OneOf.Types;

namespace Hotels.Services.Services;

public interface IHotelService
{
    Task<OneOf<HotelDto, NotFound>> GetByIdAsync(Guid id, CancellationToken token);

    Task<OneOf<HotelDto, NotFound>> GetBySlugAsync(string slug, CancellationToken token);

    Task<IEnumerable<HotelDto>> GetAllAsync(GetAllHotelsOptions options, CancellationToken token);

    Task<OneOf<Success, ValidationErrors>> CreateAsync(HotelDto hotel, CancellationToken token);

    Task<OneOf<HotelDto, NotFound, ValidationErrors>> UpdateAsync(HotelDto hotel, CancellationToken token);

    Task<OneOf<Success, NotFound>> DeleteByIdAsync(Guid id, CancellationToken token);
}
