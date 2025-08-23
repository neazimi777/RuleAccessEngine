using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService.Abstractions
{
    public interface IRuleService
    {
        Task<bool> CreateAsync(RuleDto req);
        Task<RuleDto?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<RuleDto>> ListAsync();
        Task<bool> UpdateAsync(Guid id, RuleDto req);
        Task<bool> DeleteAsync(Guid id);
    }
}
