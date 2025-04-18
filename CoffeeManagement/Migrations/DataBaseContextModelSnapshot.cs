﻿// <auto-generated />
using System;
using CoffeeManagement.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoffeeManagement.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoffeeManagement.Model.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.HasIndex("RoleId");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("CoffeeManagement.Model.AccountShift", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("ShiftId")
                        .HasColumnType("int");

                    b.HasKey("AccountId", "ShiftId");

                    b.HasIndex("ShiftId");

                    b.ToTable("accountShifts");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Beverage", b =>
                {
                    b.Property<int>("BeverageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BeverageId"));

                    b.Property<string>("BeverageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Desciption")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("BeverageId");

                    b.ToTable("beverages");
                });

            modelBuilder.Entity("CoffeeManagement.Model.BeverageMaterial", b =>
                {
                    b.Property<int>("BeverageId")
                        .HasColumnType("int");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<decimal>("QuantityUsed")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BeverageId", "MaterialId");

                    b.HasIndex("MaterialId");

                    b.ToTable("beverageMaterials");
                });

            modelBuilder.Entity("CoffeeManagement.Model.BeveragesOrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .HasColumnType("int");

                    b.Property<int>("BeverageId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId", "BeverageId");

                    b.HasIndex("BeverageId");

                    b.ToTable("beveragesOrderDetail");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialId"));

                    b.Property<DateTime>("DateOfManufacture")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaterialType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ShopPhone")
                        .HasColumnType("int");

                    b.HasKey("MaterialId");

                    b.ToTable("materials");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.HasIndex("AccountId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("CoffeeManagement.Model.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.ToTable("orderDetail");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Shift", b =>
                {
                    b.Property<int>("ShiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShiftId"));

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("WorkingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ShiftId");

                    b.ToTable("shifts");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Account", b =>
                {
                    b.HasOne("CoffeeManagement.Model.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CoffeeManagement.Model.AccountShift", b =>
                {
                    b.HasOne("CoffeeManagement.Model.Account", "Account")
                        .WithMany("AccountShifts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeManagement.Model.Shift", "Shift")
                        .WithMany("AccountShifts")
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Shift");
                });

            modelBuilder.Entity("CoffeeManagement.Model.BeverageMaterial", b =>
                {
                    b.HasOne("CoffeeManagement.Model.Beverage", "Beverage")
                        .WithMany("BeverageMaterials")
                        .HasForeignKey("BeverageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeManagement.Model.Material", "Material")
                        .WithMany("BeverageMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beverage");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("CoffeeManagement.Model.BeveragesOrderDetail", b =>
                {
                    b.HasOne("CoffeeManagement.Model.Beverage", "Beverage")
                        .WithMany("BeverageOrderDetails")
                        .HasForeignKey("BeverageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeManagement.Model.OrderDetail", "OrderDetail")
                        .WithMany("BeveragesOrders")
                        .HasForeignKey("OrderDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beverage");

                    b.Navigation("OrderDetail");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Order", b =>
                {
                    b.HasOne("CoffeeManagement.Model.Account", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("CoffeeManagement.Model.OrderDetail", b =>
                {
                    b.HasOne("CoffeeManagement.Model.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Account", b =>
                {
                    b.Navigation("AccountShifts");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Beverage", b =>
                {
                    b.Navigation("BeverageMaterials");

                    b.Navigation("BeverageOrderDetails");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Material", b =>
                {
                    b.Navigation("BeverageMaterials");
                });

            modelBuilder.Entity("CoffeeManagement.Model.OrderDetail", b =>
                {
                    b.Navigation("BeveragesOrders");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("CoffeeManagement.Model.Shift", b =>
                {
                    b.Navigation("AccountShifts");
                });
#pragma warning restore 612, 618
        }
    }
}
