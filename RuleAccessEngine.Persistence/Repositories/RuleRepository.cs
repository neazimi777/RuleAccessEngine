using RuleAccessEngine.Domain;
using RuleAccessEngine.Domain.Repositories;

namespace RuleAccessEngine.Persistence.Repositories
{
    public class RuleRepository : GenericRepository<Rule>,IRuleRepository
    {
        public RuleRepository(IRuleAccessDBContext dbContext) : base(dbContext)
        {
        }
    }
}
