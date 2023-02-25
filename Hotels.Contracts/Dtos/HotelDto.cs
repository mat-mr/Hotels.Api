namespace Hotels.Data.Models
{
    public class HotelDto
    {
        public required Guid Id { get; init; }

        public required string Name { get; set; }

        public required string Category { get; set; }

        public required bool IncludesTransfers { get; set; }

        public List<string> Rooms { get; init; } = new();
    }
}
