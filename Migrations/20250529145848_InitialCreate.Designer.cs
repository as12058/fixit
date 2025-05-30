﻿// <auto-generated />
using System;
using Fixit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fixit.Migrations
{
    [DbContext(typeof(FixitDbContext))]
    [Migration("20250529145848_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fixit.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("CityId")
                        .HasColumnType("int")
                        .HasColumnName("city_id");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customer_id");

                    b.Property<string>("Notes")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<int?>("ProfessionalId")
                        .HasColumnType("int")
                        .HasColumnName("Professional_id");

                    b.Property<int?>("ProfessionalRating")
                        .HasColumnType("int")
                        .HasColumnName("Professional_rating");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int")
                        .HasColumnName("Service_id");

                    b.Property<DateOnly?>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("Start_date");

                    b.Property<byte[]>("StartTime")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("Start_time");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("Id")
                        .HasName("PK__Bookings__3213E83F558EE572");

                    b.HasIndex("CityId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProfessionalId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Bookings", "fixit_schema");
                });

            modelBuilder.Entity("Fixit.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("PK__CATEGORI__3213E83FC5E0C72F");

                    b.ToTable("CATEGORIES", "fixit_schema");
                });

            modelBuilder.Entity("Fixit.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("PK__Cities__3213E83F2A1DADC9");

                    b.ToTable("Cities", "fixit_schema");
                });

            modelBuilder.Entity("Fixit.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("PK__CUSTOMER__3213E83F23DDC27B");

                    b.HasIndex("UserId");

                    b.ToTable("CUSTOMERS", "fixit_schema");
                });

            modelBuilder.Entity("Fixit.Models.Professional", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("Availability")
                        .HasColumnType("int")
                        .HasColumnName("availability");

                    b.Property<string>("Experience")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("experience");

                    b.Property<int?>("HourlyRate")
                        .HasColumnType("int")
                        .HasColumnName("hourly_rate");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("PK__PROFESSI__3213E83F986AA083");

                    b.HasIndex("UserId");

                    b.ToTable("PROFESSIONALS", "fixit_schema");
                });

            modelBuilder.Entity("Fixit.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("Category_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("PK__SERVICES__3213E83F18C24640");

                    b.HasIndex("CategoryId");

                    b.ToTable("SERVICES", "fixit_schema");
                });

            modelBuilder.Entity("Fixit.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("address");

                    b.Property<int?>("CityId")
                        .HasColumnType("int")
                        .HasColumnName("city_id");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("first_name");

                    b.Property<string>("GoogleId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone_number");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("User_Name");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("user_type");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex(new[] { "Email" }, "UQ__USERS__AB6E61641FB6F851")
                        .IsUnique();

                    b.ToTable("USERS", "fixit_schema");
                });

            modelBuilder.Entity("ProfCity", b =>
                {
                    b.Property<int>("ProfessionalId")
                        .HasColumnType("int")
                        .HasColumnName("Professional_id");

                    b.Property<int>("CityId")
                        .HasColumnType("int")
                        .HasColumnName("city_id");

                    b.HasKey("ProfessionalId", "CityId")
                        .HasName("PK__Prof_cit__5828C9A5407873A4");

                    b.HasIndex("CityId");

                    b.ToTable("Prof_cities", "fixit_schema");
                });

            modelBuilder.Entity("ProfService", b =>
                {
                    b.Property<int>("ProfessionalId")
                        .HasColumnType("int")
                        .HasColumnName("Professional_id");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int")
                        .HasColumnName("SERVICE_id");

                    b.HasKey("ProfessionalId", "ServiceId")
                        .HasName("PK__Prof_SER__FB1ACB8A18C0FEE6");

                    b.HasIndex("ServiceId");

                    b.ToTable("Prof_SERVICES", "fixit_schema");
                });

            modelBuilder.Entity("Fixit.Models.Booking", b =>
                {
                    b.HasOne("Fixit.Models.City", "City")
                        .WithMany("Bookings")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK__Bookings__city_i__42E1EEFE");

                    b.HasOne("Fixit.Models.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK__Bookings__custom__40058253");

                    b.HasOne("Fixit.Models.Professional", "Professional")
                        .WithMany("Bookings")
                        .HasForeignKey("ProfessionalId")
                        .HasConstraintName("FK__Bookings__Profes__40F9A68C");

                    b.HasOne("Fixit.Models.Service", "Service")
                        .WithMany("Bookings")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("FK__Bookings__Servic__41EDCAC5");

                    b.Navigation("City");

                    b.Navigation("Customer");

                    b.Navigation("Professional");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Fixit.Models.Customer", b =>
                {
                    b.HasOne("Fixit.Models.User", "User")
                        .WithMany("Customers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__CUSTOMERS__user___367C1819");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fixit.Models.Professional", b =>
                {
                    b.HasOne("Fixit.Models.User", "User")
                        .WithMany("Professionals")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__PROFESSIO__user___395884C4");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fixit.Models.Service", b =>
                {
                    b.HasOne("Fixit.Models.Category", "Category")
                        .WithMany("Services")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__SERVICES__Catego__2B0A656D");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Fixit.Models.User", b =>
                {
                    b.HasOne("Fixit.Models.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK__USERS__city_id__339FAB6E");

                    b.Navigation("City");
                });

            modelBuilder.Entity("ProfCity", b =>
                {
                    b.HasOne("Fixit.Models.City", null)
                        .WithMany()
                        .HasForeignKey("CityId")
                        .IsRequired()
                        .HasConstraintName("FK__Prof_citi__city___46B27FE2");

                    b.HasOne("Fixit.Models.Professional", null)
                        .WithMany()
                        .HasForeignKey("ProfessionalId")
                        .IsRequired()
                        .HasConstraintName("FK__Prof_citi__Profe__45BE5BA9");
                });

            modelBuilder.Entity("ProfService", b =>
                {
                    b.HasOne("Fixit.Models.Professional", null)
                        .WithMany()
                        .HasForeignKey("ProfessionalId")
                        .IsRequired()
                        .HasConstraintName("FK__Prof_SERV__Profe__498EEC8D");

                    b.HasOne("Fixit.Models.Service", null)
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .IsRequired()
                        .HasConstraintName("FK__Prof_SERV__SERVI__4A8310C6");
                });

            modelBuilder.Entity("Fixit.Models.Category", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("Fixit.Models.City", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Fixit.Models.Customer", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Fixit.Models.Professional", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Fixit.Models.Service", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Fixit.Models.User", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Professionals");
                });
#pragma warning restore 612, 618
        }
    }
}
