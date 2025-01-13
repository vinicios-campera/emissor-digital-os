using System.ComponentModel;

namespace OrderService.Domain.Enums
{
    public enum Person
    {
        [Description("Physical")]
        Physical,

        [Description("Legal")]
        Legal,

        [Description("Unknown")]
        Unknown,
    }
}