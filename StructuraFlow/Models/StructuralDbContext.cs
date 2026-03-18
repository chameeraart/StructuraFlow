using Microsoft.EntityFrameworkCore;

namespace StructuraFlow.Models
{
    public class StructuralDbContext : DbContext
    {
        public StructuralDbContext(DbContextOptions<StructuralDbContext> options) : base(options)
        {

        }
        public DbSet<ValidationRule> ValidationRules { get; set; }


    }
}
