using OrderService.Domain.Dto.Response;

namespace OrderService.Application.Interfaces.Services
{
    public interface ILocalizationService
    {
        CityResponse? GetCityByCep(string cep);
    }
}