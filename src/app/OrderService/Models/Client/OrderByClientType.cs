using System.ComponentModel;

namespace OrderService.Models.Client
{
    public enum OrderByClientType
    {
        [Description("Nome/Razão social")]
        Name,

        [Description("Antigas primeiro")]
        OldestFirst,

        [Description("Novas primeiro")]
        YoungestFirst,
    }
}