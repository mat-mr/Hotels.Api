namespace Hotels.Data.Models
{
    public class GetAllHotelsOptions
    {
        public required string? Name { get; init; }

        public required string? Category { get; init; }

        public required bool? IncludesTransfers { get; init; }
    }
}