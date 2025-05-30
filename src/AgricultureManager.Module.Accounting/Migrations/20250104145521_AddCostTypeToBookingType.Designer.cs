﻿// <auto-generated />
using System;
using AgricultureManager.Module.Accounting.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgricultureManager.Module.Accounting.Migrations
{
    [DbContext(typeof(AccountingDbContext))]
    [Migration("20250104145521_AddCostTypeToBookingType")]
    partial class AddCostTypeToBookingType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AccountHolder")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Bic")
                        .HasColumnType("longtext");

                    b.Property<string>("Blz")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HbciVersion")
                        .HasColumnType("longtext");

                    b.Property<string>("Iban")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LatestSynchronisation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AccountingAccount");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.AccountMouvement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasDefaultValueSql("UUID()");

                    b.Property<string>("AccountCode")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(15, 2)");

                    b.Property<string>("BankCode")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CustomerRef")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(700)
                        .HasColumnType("varchar(700)");

                    b.Property<string>("EndToEndId")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("InputDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MandateId")
                        .HasColumnType("longtext");

                    b.Property<string>("MessageId")
                        .HasColumnType("longtext");

                    b.Property<string>("PartnerName")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("PaymentInformationId")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Pending")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Primanota")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("ProprietaryRef")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Storno")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Text")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TextKeyAddition")
                        .HasColumnType("longtext");

                    b.Property<string>("TransactionTypeId")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("TypeCode")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("ValueDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasAlternateKey("InputDate", "Amount", "Description");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountingAccountMouvements");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountMouvementId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(15, 2)");

                    b.Property<Guid>("BookingTypeId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TaxRateId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("AccountMouvementId");

                    b.HasIndex("BookingTypeId");

                    b.HasIndex("TaxRateId");

                    b.ToTable("AccountingBooking");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.BookingType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CostType")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Short")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("AccountingBookingType");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountMouvementId")
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("Content")
                        .HasColumnType("longblob");

                    b.Property<string>("Documentname")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Documentpath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountMouvementId");

                    b.ToTable("AccountingDocument");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.StatementOfAccountDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("Content")
                        .HasColumnType("longblob");

                    b.Property<string>("Documentname")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Documentpath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Month", "Year");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountingStatementOfAccountDocument");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.TaxRate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TaxRateName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("TaxRateValue")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("Id");

                    b.ToTable("AccountingTaxRate");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.AccountMouvement", b =>
                {
                    b.HasOne("AgricultureManager.Module.Accounting.Domain.Account", "Account")
                        .WithMany("AccountMouvements")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.Booking", b =>
                {
                    b.HasOne("AgricultureManager.Module.Accounting.Domain.AccountMouvement", "AccountMouvement")
                        .WithMany("Bookings")
                        .HasForeignKey("AccountMouvementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Module.Accounting.Domain.BookingType", "BookingType")
                        .WithMany()
                        .HasForeignKey("BookingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgricultureManager.Module.Accounting.Domain.TaxRate", "TaxRate")
                        .WithMany()
                        .HasForeignKey("TaxRateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountMouvement");

                    b.Navigation("BookingType");

                    b.Navigation("TaxRate");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.Document", b =>
                {
                    b.HasOne("AgricultureManager.Module.Accounting.Domain.AccountMouvement", "AccountMouvement")
                        .WithMany("Documents")
                        .HasForeignKey("AccountMouvementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountMouvement");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.StatementOfAccountDocument", b =>
                {
                    b.HasOne("AgricultureManager.Module.Accounting.Domain.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.Account", b =>
                {
                    b.Navigation("AccountMouvements");
                });

            modelBuilder.Entity("AgricultureManager.Module.Accounting.Domain.AccountMouvement", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
