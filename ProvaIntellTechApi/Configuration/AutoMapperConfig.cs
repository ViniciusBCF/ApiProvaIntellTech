using AutoMapper;
using ProvaIntellTechApi.Domain.Entities;
using ProvaIntellTechApi.Service.DTOs;
using ProvaIntellTechApi.Service.ViewModel;

namespace ProvaIntellTechApi.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Atividade, AtividadeViewModel>().ReverseMap();
            CreateMap<AtividadeDto, AtividadeViewModel>().ReverseMap();
        }
    }
}
