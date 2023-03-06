namespace Ixora_REST_API.ApiRoutes
{
    public static class Routes
    {
        public static class Clients
        {
            public const string GetAll = "api/clients";
            public const string CreateClient = "api/clients";
            public const string Get = "api/clients/{clientId}";
            public const string Update = "api/clients/{clientId}";
            public const string Delete = "api/clients/{clientId}";
            public const string GetClientOrders = "api/clients/{clientId}/orders";
        }
        public static class Orders
        {
            public const string GetAll = "api/orders";
            public const string CreateOrder = "api/orders";
            public const string Get = "api/orders/{orderId}";
            public const string Update = "api/orders/{orderId}";
            public const string Delete = "api/orders/{orderId}";
        }
        public static class Details
        {
            public const string GetAll = "api/order/{orderId}/details";
            public const string CreateDetails = "api/orders/{orderId}/details/{detailsId}";
            public const string Get = "api/orders/{orderId}/details/{detailsId}";
            public const string Update = "api/orders/{orderId}/details/{detailsId}";
            public const string Delete = "api/orders/{orderId}/details/{detailsId}";
        }
        public static class Goods
        {
            public const string GetAll = "api/goods";
            public const string Create = "api/goods";
            public const string Get = "api/goods/{goodsId}";
            public const string Update = "api/goods/{goodsId}";
            public const string Delete = "api/goods/{goodsId}";
        }
        public static class GoodsTypes
        {
            public const string GetAll = "api/goods-types";
            public const string Create = "api/goods-types";
            public const string Get = "api/goods-types/{goodsTypeId}";
            public const string Update = "api/goods-types/{goodsTypeId}";
            public const string Delete = "api/goods-types/{goodsTypeId}";
        }
    }
}
