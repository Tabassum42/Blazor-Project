﻿// <auto-generated />
using System;
using Metarials_BlazorApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MetarialsBlazorApp.Server.Migrations
{
    [DbContext(typeof(ClientDbContext))]
    [Migration("20221219035700_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Metarials_BlazorApp.Shared.Models.Clients", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ClientID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Metarials_BlazorApp.Shared.Models.Metarials", b =>
                {
                    b.Property<int>("MetarialID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MetarialID"));

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("MetarialName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("MetarialID");

                    b.ToTable("Metarials");
                });

            modelBuilder.Entity("Metarials_BlazorApp.Shared.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("date");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("ClientID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Metarials_BlazorApp.Shared.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("MetarialID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderID", "MetarialID");

                    b.HasIndex("MetarialID");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Metarials_BlazorApp.Shared.Models.Order", b =>
                {
                    b.HasOne("Metarials_BlazorApp.Shared.Models.Clients", "Clients")
                        .WithMany("Orders")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clients");
                });

            modelBuilder.Entity("Metarials_BlazorApp.Shared.Models.OrderItem", b =>
                {
                    b.HasOne("Metarials_BlazorApp.Shared.Models.Metarials", "Metarials")
                        .WithMany("OrderItems")
                        .HasForeignKey("MetarialID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Metarials_BlazorApp.Shared.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Metarials");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Metarials_BlazorApp.Shared.Models.Clients", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Metarials_BlazorApp.Shared.Models.Metarials", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Metarials_BlazorApp.Shared.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}