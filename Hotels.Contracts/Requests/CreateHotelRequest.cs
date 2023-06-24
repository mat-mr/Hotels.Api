namespace Hotels.Contracts.Responses;

public class CreateHotelRequest
{
    public required string Name { get; init; }

    public required string Category { get; init; }

    public required bool IncludesTransfers { get; init; }
}
