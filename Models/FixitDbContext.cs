using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Models;

public partial class FixitDbContext : DbContext
{
    public FixitDbContext()
    {
    }

    public FixitDbContext(DbContextOptions<FixitDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Professional> Professionals { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3213E83F558EE572");

            entity.ToTable("Bookings", "fixit_schema");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ProfessionalId).HasColumnName("Professional_id");
            entity.Property(e => e.ProfessionalRating).HasColumnName("Professional_rating");
            entity.Property(e => e.ServiceId).HasColumnName("Service_id");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");
            entity.Property(e => e.StartTime)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("Start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.City).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__Bookings__city_i__42E1EEFE");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Bookings__custom__40058253");

            entity.HasOne(d => d.Professional).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ProfessionalId)
                .HasConstraintName("FK__Bookings__Profes__40F9A68C");

            entity.HasOne(d => d.Service).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Bookings__Servic__41EDCAC5");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CATEGORI__3213E83FC5E0C72F");

            entity.ToTable("CATEGORIES", "fixit_schema");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3213E83F2A1DADC9");

            entity.ToTable("Cities", "fixit_schema");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3213E83F23DDC27B");

            entity.ToTable("CUSTOMERS", "fixit_schema");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CUSTOMERS__user___367C1819");
        });

        modelBuilder.Entity<Professional>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PROFESSI__3213E83F986AA083");

            entity.ToTable("PROFESSIONALS", "fixit_schema");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Availability).HasColumnName("availability");
            entity.Property(e => e.Experience)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("experience");
            entity.Property(e => e.HourlyRate).HasColumnName("hourly_rate");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Professionals)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PROFESSIO__user___395884C4");

            entity.HasMany(d => d.Cities).WithMany(p => p.Professionals)
                .UsingEntity<Dictionary<string, object>>(
                    "ProfCity",
                    r => r.HasOne<City>().WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Prof_citi__city___46B27FE2"),
                    l => l.HasOne<Professional>().WithMany()
                        .HasForeignKey("ProfessionalId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Prof_citi__Profe__45BE5BA9"),
                    j =>
                    {
                        j.HasKey("ProfessionalId", "CityId").HasName("PK__Prof_cit__5828C9A5407873A4");
                        j.ToTable("Prof_cities", "fixit_schema");
                        j.IndexerProperty<int>("ProfessionalId").HasColumnName("Professional_id");
                        j.IndexerProperty<int>("CityId").HasColumnName("city_id");
                    });

            entity.HasMany(d => d.Services).WithMany(p => p.Professionals)
                .UsingEntity<Dictionary<string, object>>(
                    "ProfService",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Prof_SERV__SERVI__4A8310C6"),
                    l => l.HasOne<Professional>().WithMany()
                        .HasForeignKey("ProfessionalId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Prof_SERV__Profe__498EEC8D"),
                    j =>
                    {
                        j.HasKey("ProfessionalId", "ServiceId").HasName("PK__Prof_SER__FB1ACB8A18C0FEE6");
                        j.ToTable("Prof_SERVICES", "fixit_schema");
                        j.IndexerProperty<int>("ProfessionalId").HasColumnName("Professional_id");
                        j.IndexerProperty<int>("ServiceId").HasColumnName("SERVICE_id");
                    });
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SERVICES__3213E83F18C24640");

            entity.ToTable("SERVICES", "fixit_schema");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.Category).WithMany(p => p.Services)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__SERVICES__Catego__2B0A656D");
        });

        modelBuilder.Entity<User>(entity =>
        {

            // Add this line to explicitly set Id as primary key and IDENTITY
            entity.HasKey(e => e.Id); // Ensure Id is recognized as the primary key
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd() // <-- This is the key change: tells EF Core to make it IDENTITY
                .HasColumnName("id");  // Explicitly map to 'id' column name



            entity.ToTable("USERS", "fixit_schema");

            entity.HasIndex(e => e.Email, "UQ__USERS__AB6E61641FB6F851").IsUnique();


            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("User_Name");
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_type");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__USERS__city_id__339FAB6E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
