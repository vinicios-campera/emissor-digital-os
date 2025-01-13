using System.ComponentModel;

namespace OrderService.Models.Item
{
    public enum OrderByItemType
    {
        [Description("Nome")]
        Name,

        [Description("Recentes primeiros")]
        Insert,

        [Description("Mais caros primeiro")]
        ExpensiveFirst,

        [Description("Mais barato primeiro")]
        UnexpensiveFirst
    }
}