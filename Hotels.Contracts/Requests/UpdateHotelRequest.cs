namespace Hotels.Contracts.Requests
{
    public class UpdateHotelRequest
    {
        public required string Name { get; init; }

        public required string Category { get; init; }

        public required bool IncludesTransfers { get; init; }
    }
}
