using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Models
{
    public class CustomerContext:DbContext
    {

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }
        public DbSet<Customer> Customer { get; set; } = null!;
    }
}
