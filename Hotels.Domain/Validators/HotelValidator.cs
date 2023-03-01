using FluentValidation;
using Hotels.Data.Models;
using Hotels.Data.Repositores;

namespace Hotels.Services.Validators
{
    public class HotelValidator : AbstractValidator<HotelDto>
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelValidator(IHotelRepository hotelRepository)
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .MustAsync(ValidateName);

            RuleFor(x => x.Category)
                .NotEmpty();

            _hotelRepository = hotelRepository;
        }

        private Task<bool> ValidateName(HotelDto hotel, string name, CancellationToken token)
        {
            // check if one with same name exists

            return Task.FromResult(true);
        }
    }
}
