using OrderService.Api.Client;

namespace OrderService.Models.Client
{
    public class FilterClientsPopupResult
    {
        public string? Name { get; set; }
        public FilterClientType ClientType { get; set; }
        public DateTime? RegisterIn { get; set; }
        public DateTime? RegisterUntil { get; set; }
        public OrderByClientType OrderBy { get; set; }

        public string? GetFilterOData()
        {
            var filter = new List<string>();

            if (!string.IsNullOrEmpty(Name))
                filter.Add($"contains(toUpper({nameof(ClientResponse.Name)}), toUpper('{Name}'))");

            if (ClientType != FilterClientType.All)
                filter.Add($"{nameof(ClientResponse.Type)} eq '{ClientType}'");

            if (RegisterIn.HasValue)
                filter.Add($"{nameof(ClientResponse.Inserted)} ge {RegisterIn.Value:yyyy-MM-dd}");

            if (RegisterUntil.HasValue)
                filter.Add($"{nameof(ClientResponse.Inserted)} le {RegisterUntil.Value:yyyy-MM-dd}");

            if (filter.Any())
                return string.Join(" AND ", filter);

            return null;
        }

        public string? GetOrderOData()
        {
            return OrderBy switch
            {
                OrderByClientType.Name => $"{nameof(ClientResponse.Name)} asc",
                OrderByClientType.OldestFirst => $"{nameof(ClientResponse.Inserted)} desc",
                OrderByClientType.YoungestFirst => $"{nameof(ClientResponse.Inserted)} asc",
                _ => null,
            };
        }
    }
}