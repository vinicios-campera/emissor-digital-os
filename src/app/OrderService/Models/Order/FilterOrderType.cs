using System.ComponentModel;

namespace OrderService.Models.Order
{
    public enum FilterOrderType
    {
        [Description("Todas")]
        All,

        [Description("Não pagas")]
        None,

        [Description("Pagas")]
        Pay,
    }
}