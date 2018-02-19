using acscustomersgatebackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace acscustomersgatebackend.Migrations
{
    [DbContext(typeof(CustomersGateContext))]
    [Migration("20180208122952_AddOrdersTable")]
    partial class AddOrdersTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<int>("merchant_order_id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("amount_cents");

                    b.Property<string>("currency");

                    b.Property<bool>("delivery_needed");

                    b.Property<int>("merchant_id");

                    b.Property<int>("shipping_dataId");

                    b.HasKey("merchant_order_id");

                    b.HasIndex("shipping_dataId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ShippingData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("apartment");

                    b.Property<string>("building");

                    b.Property<string>("city");

                    b.Property<string>("country");

                    b.Property<string>("email");

                    b.Property<string>("first_name");

                    b.Property<string>("floor");

                    b.Property<string>("last_name");

                    b.Property<string>("phone_number");

                    b.Property<string>("postal_code");

                    b.Property<string>("state");

                    b.Property<string>("street");

                    b.HasKey("id");

                    b.ToTable("ShippingData");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("ShippingData", "shipping_data")
                        .WithMany()
                        .HasForeignKey("shipping_dataId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
