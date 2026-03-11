using EfCoreCodeFirstApproachDemo01;
using Microsoft.EntityFrameworkCore;

public class EmployeeDBContext : DbContext
{
    public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
    {
    }

    public DbSet<EmployeeModel> employees { get; set; }
}