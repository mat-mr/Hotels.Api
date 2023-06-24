using Hotels.Data.Models;
using Hotels.Data.Repositores;
using Hotels.Services.Validators;

namespace Hotel.Services.Tests;

public class HotelValidatorTests
{
    private Mock<IHotelRepository> _hotelRepository = new Mock<IHotelRepository>();

    private HotelValidator _sut;

    public HotelValidatorTests()
    {
        _sut = new HotelValidator(_hotelRepository.Object);
    }

    [Fact]
    public async Task ValidateAsync_GivenEmptyId_ShouldInvalidate()
    {
        // Arrange
        var hotel = new HotelDto
        {
            Id = Guid.Empty,
            Name = "Test Name",
            Category = "category",
            IncludesTransfers = true
        };
        
        // act
        var actual = await _sut.ValidateAsync(hotel, new CancellationToken());

        // Assert
        actual.IsValid.Should().Be(false);
        actual.Errors.FirstOrDefault()?.ErrorMessage.Should().Be($"'{nameof(hotel.Id)}' must not be empty.");
    }

    [Fact]
    public async Task ValidateAsync_GivenEmptyName_ShouldInvalidate()
    {
        // Arrange
        var hotel = new HotelDto
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Category = "category",
            IncludesTransfers = true
        };

        // act
        var actual = await _sut.ValidateAsync(hotel, new CancellationToken());

        // Assert
        actual.IsValid.Should().Be(false);
        actual.Errors.FirstOrDefault()?.ErrorMessage.Should().Be($"'{nameof(hotel.Name)}' must not be empty.");
    }

    [Fact]
    public async Task ValidateAsync_GivenNameAlreadyExists_ShouldInvalidate()
    {
        // Arrange
        var newHotelId = Guid.NewGuid();
        var existingHotelId = Guid.NewGuid();

        var hotel = new HotelDto
        {
            Id = newHotelId,
            Name = "Test Hotel",
            Category = "category",
            IncludesTransfers = true
        };
        _hotelRepository.Setup(m => m.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HotelDto
            {
                Id = existingHotelId,
                Name = "Test Hotel",
                Category = "category",
                IncludesTransfers = true
            });

        // act
        var actual = await _sut.ValidateAsync(hotel, new CancellationToken());

        // Assert
        actual.IsValid.Should().Be(false);
        actual.Errors.FirstOrDefault()?.ErrorMessage.Should().Be($"The specified condition was not met for '{nameof(hotel.Name)}'.");
    }

    [Fact]
    public async Task ValidateAsync_GivenNameIsFoundButForSameHotel_ShouldInvalidate()
    {
        // Arrange
        var hotelId = Guid.NewGuid();

        var hotel = new HotelDto
        {
            Id = hotelId,
            Name = "Test Hotel",
            Category = "category",
            IncludesTransfers = true
        };
        _hotelRepository.Setup(m => m.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HotelDto
            {
                Id = hotelId,
                Name = "Test Hotel",
                Category = "category",
                IncludesTransfers = true
            });

        // act
        var actual = await _sut.ValidateAsync(hotel, new CancellationToken());

        // Assert
        actual.IsValid.Should().Be(true);
    }

    [Fact]
    public async Task ValidateAsync_GivenEmptyCategory_ShouldInvalidate()
    {
        // Arrange
        var hotel = new HotelDto
        {
            Id = Guid.NewGuid(),
            Name = "test name",
            Category = string.Empty,
            IncludesTransfers = true
        };

        // act
        var actual = await _sut.ValidateAsync(hotel, new CancellationToken());

        // Assert
        actual.IsValid.Should().Be(false);
        actual.Errors.FirstOrDefault()?.ErrorMessage.Should().Be($"'{nameof(hotel.Category)}' must not be empty.");
    }

    [Fact]
    public async Task ValidateAsync_GivenAllPropertiesOk_ShouldreturnValid()
    {
        // Arrange
        var hotel = new HotelDto
        {
            Id = Guid.NewGuid(),
            Name = "test name",
            Category = "category",
            IncludesTransfers = true
        };

        _hotelRepository.Setup(m => m.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as HotelDto);

        // act
        var actual = await _sut.ValidateAsync(hotel, new CancellationToken());

        // Assert
        actual.IsValid.Should().Be(true);
    }
}