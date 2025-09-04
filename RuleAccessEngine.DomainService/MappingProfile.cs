using AutoMapper;
using RuleAccessEngine.Domain;
using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Rule, RuleDto>().ReverseMap();
            CreateMap<EvaluateResult, EvaluateResponseDto>().ReverseMap();
        }
    }
}
