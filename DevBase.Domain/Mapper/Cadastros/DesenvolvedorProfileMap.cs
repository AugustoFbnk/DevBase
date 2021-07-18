using AutoMapper;
using DevBase.Domain.DTO.Cadastros;
using DevBase.Domain.Entidades.Cadastros;
using DevBase.Util.ExtensionMethods;

namespace DevBase.Domain.Mapper.Cadastros
{
    public class DesenvolvedorProfileMap : Profile
    {
        public DesenvolvedorProfileMap()
        {
            CreateMap<Desenvolvedor, DesenvolvedorDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.DataNascimento, opts => opts.MapFrom(src => src.DataNascimento))
                .ForMember(dest => dest.Hobby, opts => opts.MapFrom(src => src.Hobby))
                .ForMember(dest => dest.Idade, opts => opts.MapFrom(src => src.Idade))
                .ForMember(dest => dest.Nome, opts => opts.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Sexo, opts => opts.MapFrom(src => src.Sexo.ToEnumChar()));

            CreateMap<DesenvolvedorDto, Desenvolvedor>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.DataNascimento, opts => opts.MapFrom(src => src.DataNascimento))
                .ForMember(dest => dest.Hobby, opts => opts.MapFrom(src => src.Hobby))
                .ForMember(dest => dest.Idade, opts => opts.MapFrom(src => src.DataNascimento.CalcularIdade()))
                .ForMember(dest => dest.Nome, opts => opts.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Sexo, opts => opts.MapFrom(src => src.Sexo.ToCharEnum<Sexo>()));
        }
    }
}
