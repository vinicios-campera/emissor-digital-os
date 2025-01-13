using System.ComponentModel;

namespace OrderService.Domain.Enums
{
    public enum Measure
    {
        [Description("UN")]
        Unit,

        [Description("CM")]
        Centimeters,

        [Description("MT")]
        Meters,

        [Description("CX")]
        Box,

        [Description("KM")]
        Kilometers
    }
}