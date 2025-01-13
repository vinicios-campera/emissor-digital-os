using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Response;
using RestSharp;
using Kernel.Toolkit.Extensions;

namespace OrderService.Infrastructure.Services
{
    public class LocalizationService : ILocalizationService
    {
        public CityResponse? GetCityByCep(string cep)
        {
            if (!string.IsNullOrWhiteSpace(cep))
            {
                var options = new RestClientOptions("https://viacep.com.br")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest($"/ws/{cep.OnlyNumbers()}/json", Method.Get);
                RestResponse response = client.Execute(request);
                return response.Content!.FromJson<CityResponse>();
            }
            else
            {
                return new CityResponse();
            }
        }
    }
}