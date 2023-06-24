using System.Text.RegularExpressions;

namespace Hotels.Data.Models;

public partial class HotelDto
{
    public required Guid Id { get; init; }

    public required string Name { get; set; }

    public required string Category { get; set; }

    public required bool IncludesTransfers { get; set; }

    public List<string> Rooms { get; init; } = new();

    public string Slug => GetSlug();

    private string GetSlug()
    {
        var sluggedName = RemoveOddCharactersRegex().Replace(Name, string.Empty)
        .Replace(" ", "-")
        .ToLowerInvariant();

        return sluggedName;
    }

    [GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
    private static partial Regex RemoveOddCharactersRegex();
}
