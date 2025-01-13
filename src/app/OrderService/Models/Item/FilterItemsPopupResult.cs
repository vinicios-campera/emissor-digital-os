using OrderService.Api.Client;

namespace OrderService.Models.Item
{
    public class FilterItemsPopupResult
    {
        public string? Description { get; set; }
        public FilterItemType ItemType { get; set; }
        public OrderByItemType OrderBy { get; set; }

        public string? GetFilterOData()
        {
            var filter = new List<string>();

            if (!string.IsNullOrEmpty(Description))
                filter.Add($"contains(toUpper({nameof(ProductResponse.Description)}), toUpper('{Description}'))");

            if (ItemType != FilterItemType.All)
                filter.Add($"{nameof(ProductResponse.Measure)} eq '{ItemType}'");

            if (filter.Any())
                return string.Join(" AND ", filter);

            return null;
        }

        public string? GetOrderOData()
        {
            return OrderBy switch
            {
                OrderByItemType.Name => $"{nameof(ProductResponse.Description)} asc",
                OrderByItemType.Insert => $"{nameof(ProductResponse.Inserted)} desc",
                OrderByItemType.UnexpensiveFirst => $"{nameof(ProductResponse.UnitaryValue)} asc",
                OrderByItemType.ExpensiveFirst => $"{nameof(ProductResponse.UnitaryValue)} desc",
                _ => null,
            };
        }
    }
}