namespace Hotels.Contracts.Requests
{
    public class HotelsResponse
    {
        public required IEnumerable<HotelResponse> Items { get; init; } = Enumerable.Empty<HotelResponse>();
    }
}
