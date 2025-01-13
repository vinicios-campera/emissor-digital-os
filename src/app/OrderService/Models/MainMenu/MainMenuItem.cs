using System.ComponentModel;
using Xamarin.Forms;

namespace OrderService.Models.MainMenu
{
    public class MainMenuItem : MenuItem
    {
        public Color BackgroundColor { get; set; }
        public ItemType Type { get; set; }
    }

    public enum ItemType
    {
        [Description("Clientes")]
        Clients,

        [Description("Produtos")]
        Products,

        [Description("Ordens")]
        Orders,

        [Description("Sobre")]
        About,

        [Description("Nova O.S.")]
        NewOs,

        [Description("Novo cliente")]
        NewClient,

        [Description("Sair")]
        Logout
    }
}