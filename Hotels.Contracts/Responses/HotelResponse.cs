namespace Hotels.Contracts.Requests
{
    public class HotelResponse
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }

        public required string Category { get; init; }

        public required bool IncludesTransfers { get; init; }

        public required IEnumerable<string> Rooms { get; init; } = Enumerable.Empty<string>();
    }
}
