using FluentValidation;
using Hotels.Data.Models;
using Hotels.Data.Repositores;
using Hotels.Services.Validators;
using OneOf;
using OneOf.Types;

namespace Hotels.Services.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IValidator<HotelDto> _hotelValidator;

    public HotelService(IHotelRepository hotelRepository, IValidator<HotelDto> hotelValidator)
    {
        _hotelRepository = hotelRepository;
        _hotelValidator = hotelValidator;
    }

    public async Task<OneOf<HotelDto, NotFound>> GetByIdAsync(Guid id, CancellationToken token)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id, token);

        if(hotel is null) 
        {
            return new NotFound();
        }

        return hotel;
    }

    public async Task<OneOf<HotelDto, NotFound>> GetBySlugAsync(string slug, CancellationToken token)
    {
        var hotel = await _hotelRepository.GetBySlugAsync(slug, token);

        if (hotel is null)
        {
            return new NotFound();
        }

        return hotel;
    }

    public async Task<IEnumerable<HotelDto>> GetAllAsync(CancellationToken token)
    {
        return await _hotelRepository.GetAllAsync(token);
    }

    public async Task<OneOf<Success, ValidationErrors>> CreateAsync(HotelDto hotel, CancellationToken token)
    {
        var validationResult = await _hotelValidator.ValidateAsync(hotel, token);
        if (!validationResult.IsValid)
        {
            return new ValidationErrors(validationResult.Errors);
        }

        await _hotelRepository.CreateAsync(hotel, token);

        return new Success();
    }

    public async Task<OneOf<HotelDto, NotFound, ValidationErrors>> UpdateAsync(HotelDto hotel, CancellationToken token)
    {
        var validationResult = await _hotelValidator.ValidateAsync(hotel, token);
        if (!validationResult.IsValid)
        {
            return new ValidationErrors(validationResult.Errors);
        }

        var hotelExists = await _hotelRepository.ExistsByIdAsync(hotel.Id, token);
        if (!hotelExists)
        {
            return new NotFound();
        }

        await _hotelRepository.UpdateAsync(hotel, token);

        return hotel;
    }

    public async Task<OneOf<Success, NotFound>> DeleteByIdAsync(Guid id, CancellationToken token)
    {
        var deleted = await _hotelRepository.DeleteByIdAsync(id, token);
        
        if (!deleted)
        {
            return new NotFound();
        }

        return new Success();
    }
}
