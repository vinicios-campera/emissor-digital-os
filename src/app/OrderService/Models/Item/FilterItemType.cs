using System.ComponentModel;

namespace OrderService.Models.Item
{
    public enum FilterItemType
    {
        [Description("Todas")]
        All,

        [Description("Unidade")]
        Unit,

        [Description("Metros")]
        Meters,

        [Description("Centrimetros")]
        Centimeters,

        [Description("Caixa")]
        Box
    }
}