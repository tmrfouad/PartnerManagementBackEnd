using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using acscustomersgatebackend.Models.EntityTypeConfigurations;

namespace acscustomersgatebackend.Models
{
    public class CustomersGateContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<RFQ> RFQs { get; set; }

        public CustomersGateContext(DbContextOptions<CustomersGateContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RFQConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}