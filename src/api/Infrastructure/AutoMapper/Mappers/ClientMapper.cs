using AutoMapper;
using Kernel.Toolkit.Extensions;
using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Dto.Update;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Constants;

namespace OrderService.Infrastructure.AutoMapper.Mappers
{
    public class ClientMapper : Profile
    {
        public ClientMapper()
        {
            CreateMap<Client, ClientResponse>();

            CreateMap<ClientInsert, Client>()
                .ForMember(d => d.UserEmail, x => x.MapFrom((s, d, x, c) => c.Items[AutomapperConstants.PARAM_USER_EMAIL].ConvertTo<string>()))
                .ForMember(d => d.Name, x => x.MapFrom(s => s.Name!.ToTitleCase()))
                .AfterMap((s, d, c) => GetLocalization(d, s, c.Items[AutomapperConstants.PARAM_CITY].ConvertTo<CityResponse>()))
                .AfterMap((s, d) => d.SetInsertedDate());

            CreateMap<ClientUpdate, Client>()
                .ForMember(d => d.Name, x => x.MapFrom(s => s.Name!.ToTitleCase()))
                .AfterMap((s, d, c) => GetLocalization(d, s, c.Items[AutomapperConstants.PARAM_CITY].ConvertTo<CityResponse>()))
                .AfterMap((s, d) => d.SetModifiedDate());
        }

        private void GetLocalization(Client d, ClientInsert s, CityResponse city)
        {
            if (city.Cep != null)
            {
                d.State = city!.State;
                d.City = city!.City;
                d.Cep = city!.Cep;
            }
            else
            {
                d.State = s.State?.ToUpper();
                d.City = s.City?.ToTitleCase();
                d.Cep = s.Cep;
            }
        }

        private void GetLocalization(Client d, ClientUpdate s, CityResponse city)
        {
            if (city.Cep != null)
            {
                d.State = city!.State;
                d.City = city!.City;
                d.Cep = city!.Cep;
            }
            else
            {
                d.State = s.State?.ToUpper();
                d.City = s.City?.ToTitleCase();
                d.Cep = s.Cep;
            }
        }
    }
}