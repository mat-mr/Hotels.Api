using FluentValidation;
using Hotels.Data.Models;
using Hotels.Data.Repositores;

namespace Hotels.Services.Validators;

public class HotelValidator : AbstractValidator<HotelDto>
{
    private readonly IHotelRepository _hotelRepository;

    public HotelValidator(IHotelRepository hotelRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MustAsync(IsNameValid);

        RuleFor(x => x.Category)
            .NotEmpty();

        _hotelRepository = hotelRepository;
    }

    private async Task<bool> IsNameValid(HotelDto hotel, string name, CancellationToken token)
    {
        var hotelWithSameName = await _hotelRepository.GetByNameAsync(name, token);

        return hotelWithSameName is null 
            || hotelWithSameName.Id == hotel.Id;
    }
}
