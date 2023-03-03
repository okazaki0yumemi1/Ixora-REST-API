namespace Ixora_REST_API.ApiRoutes
{
    public static class Routes
    {
        public static class Clients
        {
            public const string GetAll = "api/clients";
            public const string CreateClient = "api/clients";
            public const string Get = "/clients/{clientId}";
            public const string Update = "/clients/{clientId}";
            public const string Delete = "/clients/{clientId}";
        }
    }
}
