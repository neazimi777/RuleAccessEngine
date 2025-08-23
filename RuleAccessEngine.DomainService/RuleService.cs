using AutoMapper;
using Microsoft.AspNetCore.Http;
using RuleAccessEngine.Domain;
using RuleAccessEngine.Domain.Enums;
using RuleAccessEngine.Domain.Exceptions;
using RuleAccessEngine.Domain.Repositories;
using RuleAccessEngine.DomainService.Abstractions;
using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService
{
    public class RuleService : IRuleService
    {
        private readonly IRuleRepository _ruleRepository;
        private readonly IMapper _mapper;

        public RuleService(IRuleRepository ruleRepository, IMapper mapper)
        {
            _ruleRepository = ruleRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(RuleDto req)
        {
            if (req is null) throw new ArgumentNullException(nameof(req));

            var entity = _mapper.Map<Rule>(req);
            var result = await _ruleRepository.CreateAsync(entity);
            if (result is not null)
               return true;

            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _ruleRepository.GetAsync(id);
            if (entity is null) throw new BaseException("rule not found ", StatusCodes.Status400BadRequest);

            var result =  _ruleRepository.Delete(entity);
            if (result is not null)
                return true;

            return false;
        }

        public async Task<RuleDto?> GetByIdAsync(Guid id)
        {
            var entity = await _ruleRepository.GetAsync(id);
            return entity is null ? null : _mapper.Map<RuleDto>(entity);
        }

        public async Task<IReadOnlyList<RuleDto>> ListAsync()
        {
            var entities = await _ruleRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<RuleDto>>(entities);
        }

        public async Task<bool> UpdateAsync(Guid id, RuleDto req)
        {
            var entity = await _ruleRepository.GetAsync(id);
            if (entity is null) throw new BaseException("rule not found ", StatusCodes.Status400BadRequest);

            _mapper.Map(req, entity);
            var result =  _ruleRepository.Update(entity);
            if (result is not null)
                return true;

            return false;
        }
    }
}
