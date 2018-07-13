using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PartnerManagement.Models.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PartnerManagement.Models
{
    public class PartnerManagementContext : IdentityDbContext
    {
        public DbSet<RFQ> RFQs { get; set; }
        public DbSet<Representative> Representatives { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailSender> EmailSenders { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<SubscriptionUser> SubscriptionUsers { get; set; }
        public DbSet<InvoiceActivity> InvoiceActivities { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public PartnerManagementContext(DbContextOptions<PartnerManagementContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RFQConfiguration());
            modelBuilder.ApplyConfiguration(new RFQActionConfiguration());
            modelBuilder.ApplyConfiguration(new RFQActionAttConfiguration());
            modelBuilder.ApplyConfiguration(new RepresentativeConfigration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEditionConfiguration());
            modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new EmailSenderConfiguration());
            modelBuilder.ApplyConfiguration(new PartnerConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionUserConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceActivityConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDetailConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}