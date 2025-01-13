using System.ComponentModel;

namespace OrderService.Models.Item
{
    public enum MeasureType
    {
        [Description("Unidade")]
        Unidade = Api.Client.Measure.Unit,

        [Description("Centimetros")]
        Centimetros = Api.Client.Measure.Centimeters,

        [Description("Metros")]
        Metros = Api.Client.Measure.Meters,

        [Description("Caixa")]
        Caixa = Api.Client.Measure.Box,
    }
}