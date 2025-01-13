using System.ComponentModel;

namespace OrderService.Models.Client
{
    public enum FilterClientType
    {
        [Description("Todas")]
        All,

        [Description("Pessoa Física")]
        Physical,

        [Description("Pessoa Júridica")]
        Legal,
    }
}