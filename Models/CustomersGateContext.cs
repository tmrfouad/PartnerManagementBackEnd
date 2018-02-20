using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using acscustomersgatebackend.Models.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace acscustomersgatebackend.Models
{
    public class CustomersGateContext : IdentityDbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<RFQ> RFQs { get; set; }

        public CustomersGateContext(DbContextOptions<CustomersGateContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RFQConfiguration());
            modelBuilder.ApplyConfiguration(new RFQActionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}