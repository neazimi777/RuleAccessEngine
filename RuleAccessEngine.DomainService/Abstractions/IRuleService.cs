using RuleAccessEngine.Domain.Enums;
using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService.Abstractions
{
    public interface IRuleService
    {
        Task<RuleDto> CreateAsync(CreateRuleRequest req, CancellationToken ct = default);
        Task<RuleDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<RuleDto>> ListAsync(
            bool? isActive = null,
            RuleType? type = null,
            string? nameSearch = null,
            CancellationToken ct = default);

        Task<bool> UpdateAsync(Guid id, UpdateRuleRequest req, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
