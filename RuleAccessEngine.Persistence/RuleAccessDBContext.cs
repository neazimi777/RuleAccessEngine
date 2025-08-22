using Microsoft.EntityFrameworkCore;
using RuleAccessEngine.Domain;
using System.Reflection;

namespace RuleAccessEngine.Persistence
{
    public class RuleAccessDBContext : DbContext, IRuleAccessDBContext
    {
        public RuleAccessDBContext(DbContextOptions<RuleAccessDBContext> options) : base(options) { }

        public DbSet<Rule> Rule { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(
                i => i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
            ));
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}


