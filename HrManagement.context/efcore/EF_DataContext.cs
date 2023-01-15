using Microsoft.EntityFrameworkCore;

namespace HrManagement.context.efcore
{
    public class EF_DataContext : DbContext
    {
        public EF_DataContext()
        {

        }
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<Employee> Employees { get; set;} 

        public DbSet<Leave> Leaves { get; set;}

    }
}
