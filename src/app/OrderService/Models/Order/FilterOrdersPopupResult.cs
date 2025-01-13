using OrderService.Api.Client;

namespace OrderService.Models.Order
{
    public class FilterOrdersPopupResult
    {
        public string? ClientName { get; set; }
        public FilterOrderType OrderType { get; set; }
        public DateTime? RegisterIn { get; set; }
        public DateTime? RegisterUntil { get; set; }
        public OrderByOrderType OrderBy { get; set; }

        public string? GetFilterOData()
        {
            var filter = new List<string>();
            if (!string.IsNullOrEmpty(ClientName))
                filter.Add($"contains(toUpper({nameof(OrderResponse.Client)}/{nameof(OrderResponse.Client.Name)}), toUpper('{ClientName}'))");

            if (OrderType != FilterOrderType.All)
                filter.Add($"{nameof(OrderResponse.State)} eq '{OrderType}'");

            if (filter.Any())
                return string.Join(" AND ", filter);

            return null;
        }

        public string? GetOrderOData()
        {
            return OrderBy switch
            {
                OrderByOrderType.ClientName => $"{nameof(OrderResponse.Client)}/{nameof(OrderResponse.Client.Name)} asc",
                OrderByOrderType.Identifier => $"{nameof(OrderResponse.Identifier)} desc",
                OrderByOrderType.PayFirst => $"{nameof(OrderResponse.State)} desc",
                OrderByOrderType.NotPayFirst => $"{nameof(OrderResponse.State)} asc",
                OrderByOrderType.YoungestFirst => $"{nameof(ClientResponse.Inserted)} asc",
                OrderByOrderType.OldestFirst => $"{nameof(ClientResponse.Inserted)} desc",
                _ => null,
            };
        }
    }
}