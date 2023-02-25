namespace Hotels.Contracts.Requests
{
    public class HotelsResponse
    {
        public required IEnumerable<HotelResponse> Rooms { get; init; } = Enumerable.Empty<HotelResponse>();
    }
}
