using System.ComponentModel;

namespace OrderService.Models.Order
{
    public enum OrderByOrderType
    {
        [Description("Numero da O.S.")]
        Identifier,

        [Description("Nome do cliente")]
        ClientName,

        [Description("Pagas primeiro")]
        PayFirst,

        [Description("Não pagas primeiro")]
        NotPayFirst,

        [Description("Novas primeiro")]
        YoungestFirst,

        [Description("Antigas primeiro")]
        OldestFirst
    }
}