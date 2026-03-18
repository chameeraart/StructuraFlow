using StructuraFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace StructuraFlow.Services
{
    public class InitMigrations
    {
        private readonly StructuralDbContext context;
        public InitMigrations(StructuralDbContext context)
        {
            this.context = context;
        }
        public void MigrateDatabase()
        {
            context.Database.Migrate();
        }
    }
}
