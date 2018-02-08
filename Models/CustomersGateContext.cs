using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class CustomersGateContext : DbContext
{
    public CustomersGateContext(DbContextOptions<CustomersGateContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
}