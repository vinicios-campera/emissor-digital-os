using System.ComponentModel;

namespace OrderService.Models.Client
{
    public enum ClientType
    {
        [Description("Pessoa Física")]
        Fisica = Api.Client.Person.Physical,

        [Description("Pessoa Juridica")]
        Juridica = Api.Client.Person.Legal
    }
}