namespace Hotels.Api;

public class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Hotels
    {
        private const string Base = $"{ApiBase}/hotels";

        public const string Get = $"{Base}/{{idOrSlug}}";

        public const string GetAll = Base;

        public const string Create = Base;

        public const string Update = $"{Base}/{{id}}";

        public const string Delete = $"{Base}/{{id}}";
    }
}
