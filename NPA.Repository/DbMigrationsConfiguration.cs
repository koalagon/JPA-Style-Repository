using System.Data.Entity.Migrations;

namespace NPA.Repository
{
    internal sealed class DbMigrationsConfiguration : DbMigrationsConfiguration<SimpleDbContext>
    {
        public DbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
