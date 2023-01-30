﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Context;

#nullable disable

namespace TosunlarYapiMarket.Data.Migrations
{
    [DbContext(typeof(TosunlarYapiMarketDbContext))]
    [Migration("20230130102910_AddedIsPaidToInvoiceTable")]
    partial class AddedIsPaidToInvoiceTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "2f0ca96f-c729-4fb6-a3d8-5a625c282665",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1f4f7b4d-321a-4dab-a0a9-5242490b9edd",
                            Email = "serdehablog@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Admin",
                            ImageUrl = "/userImages/defaultUser.png",
                            LastName = "User",
                            LockoutEnabled = false,
                            NormalizedEmail = "SERDEHABLOG@GMAIL.COM",
                            NormalizedUserName = "SERDEHA",
                            PasswordHash = "AQAAAAEAACcQAAAAEGm7pQlxUEO8ACTAcQr+sV7V04Rq5zoSrdixg+tECA6X1D5gyIFp7iTv9W5q9BBn+Q==",
                            PhoneNumber = "+905555555555",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "b9d5a1d5-1bbb-4a19-aadd-471416da98ef",
                            TwoFactorEnabled = false,
                            UserName = "serdeha"
                        });
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("BusinessAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CustomerDebt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CustomerNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Debt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Debt");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal>("DiscountPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DiscountedTotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InvoiceCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<double>("Meter")
                        .HasColumnType("float");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("SquareMeter")
                        .HasColumnType("float");

                    b.Property<double>("Ton")
                        .HasColumnType("float");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NoteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoteDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoteTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.PayOffDebt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DebtId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PaidDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DebtId");

                    b.ToTable("PayOffDebt");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("KDV")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("StockAnyDetail")
                        .HasColumnType("float");

                    b.Property<int>("StockDetailId")
                        .HasColumnType("int");

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("StockDetailId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.StockBasket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<double>("StockAnyDetail")
                        .HasColumnType("float");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<string>("StockName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("StockPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("StockId");

                    b.ToTable("StockBasket");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.StockDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedByName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StockDetailName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("StockDetails");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.UserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Customer", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppUser", "AppUser")
                        .WithMany("Customers")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Debt", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppUser", "AppUser")
                        .WithMany("Debts")
                        .HasForeignKey("AppUserId")
                        .IsRequired();

                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.Customer", "Customer")
                        .WithMany("Debts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Invoice", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppUser", "AppUser")
                        .WithMany("Invoices")
                        .HasForeignKey("AppUserId")
                        .IsRequired();

                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.Customer", "Customer")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId")
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Note", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppUser", "AppUser")
                        .WithMany("Notes")
                        .HasForeignKey("AppUserId")
                        .IsRequired();

                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.Customer", "Customer")
                        .WithMany("Notes")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.PayOffDebt", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.Debt", "Debt")
                        .WithMany("PayOffDebts")
                        .HasForeignKey("DebtId")
                        .IsRequired();

                    b.Navigation("Debt");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.RoleClaim", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Stock", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppUser", "AppUser")
                        .WithMany("Stocks")
                        .HasForeignKey("AppUserId")
                        .IsRequired();

                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.StockDetail", "StockDetail")
                        .WithMany("Stocks")
                        .HasForeignKey("StockDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("StockDetail");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.StockBasket", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.Invoice", "Invoice")
                        .WithMany("StockBaskets")
                        .HasForeignKey("InvoiceId")
                        .IsRequired();

                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.Stock", "Stock")
                        .WithMany("StockBaskets")
                        .HasForeignKey("StockId")
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.StockDetail", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppUser", "AppUser")
                        .WithMany("StockDetails")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.UserClaim", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.UserLogin", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.UserRole", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.UserToken", b =>
                {
                    b.HasOne("TosunlarYapiMarket.Entity.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.AppUser", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Debts");

                    b.Navigation("Invoices");

                    b.Navigation("Notes");

                    b.Navigation("StockDetails");

                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Customer", b =>
                {
                    b.Navigation("Debts");

                    b.Navigation("Invoices");

                    b.Navigation("Notes");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Debt", b =>
                {
                    b.Navigation("PayOffDebts");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Invoice", b =>
                {
                    b.Navigation("StockBaskets");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.Stock", b =>
                {
                    b.Navigation("StockBaskets");
                });

            modelBuilder.Entity("TosunlarYapiMarket.Entity.Concrete.StockDetail", b =>
                {
                    b.Navigation("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
