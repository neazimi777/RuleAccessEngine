using RuleAccessEngine.Domain;
using RuleAccessEngine.Domain.Repositories;
using RuleAccessEngine.Persistence.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleAccessEngine.Persistence.Repositories
{
    public class RuleCacheRepository : GenericCacheRepository<Rule>, IRuleCacheRepository
    {
        public RuleCacheRepository(IConnectionMultiplexer mux, RedisCacheOptions options) : base(mux, options)
        {
        }
    }
}
