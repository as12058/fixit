using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace fixit.Models;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=fixitDB;User Id=fixit;Password=fixit123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3213E83FA7A5E6D5");

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
                .HasConstraintName("FK__Bookings__city_i__5165187F");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Bookings__custom__4E88ABD4");

            entity.HasOne(d => d.Professional).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ProfessionalId)
                .HasConstraintName("FK__Bookings__Profes__4F7CD00D");

            entity.HasOne(d => d.Service).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Bookings__Servic__5070F446");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CATEGORI__3213E83F83CE3FF0");

            entity.ToTable("CATEGORIES");

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
            entity.HasKey(e => e.Id).HasName("PK__Cities__3213E83FCFB3D398");

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
            entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3213E83F4AA29936");

            entity.ToTable("CUSTOMERS");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CUSTOMERS__user___44FF419A");
        });

        modelBuilder.Entity<Professional>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PROFESSI__3213E83F8223EA1D");

            entity.ToTable("PROFESSIONALS");

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
                .HasConstraintName("FK__PROFESSIO__user___47DBAE45");

            entity.HasMany(d => d.Cities).WithMany(p => p.Professionals)
                .UsingEntity<Dictionary<string, object>>(
                    "ProfCity",
                    r => r.HasOne<City>().WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Prof_citi__city___5535A963"),
                    l => l.HasOne<Professional>().WithMany()
                        .HasForeignKey("ProfessionalId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Prof_citi__Profe__5441852A"),
                    j =>
                    {
                        j.HasKey("ProfessionalId", "CityId").HasName("PK__Prof_cit__5828C9A5D52439F8");
                        j.ToTable("Prof_cities");
                        j.IndexerProperty<int>("ProfessionalId").HasColumnName("Professional_id");
                        j.IndexerProperty<int>("CityId").HasColumnName("city_id");
                    });

            entity.HasMany(d => d.Services).WithMany(p => p.Professionals)
                .UsingEntity<Dictionary<string, object>>(
                    "ProfService",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Prof_SERV__SERVI__59063A47"),
                    l => l.HasOne<Professional>().WithMany()
                        .HasForeignKey("ProfessionalId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Prof_SERV__Profe__5812160E"),
                    j =>
                    {
                        j.HasKey("ProfessionalId", "ServiceId").HasName("PK__Prof_SER__FB1ACB8A52826633");
                        j.ToTable("Prof_SERVICES");
                        j.IndexerProperty<int>("ProfessionalId").HasColumnName("Professional_id");
                        j.IndexerProperty<int>("ServiceId").HasColumnName("SERVICE_id");
                    });
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SERVICES__3213E83FB7BDBDAD");

            entity.ToTable("SERVICES");

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
                .HasConstraintName("FK__SERVICES__Catego__398D8EEE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__3213E83FEF2A57B5");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Email, "UQ__USERS__AB6E6164222E25C6").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
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
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_type");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__USERS__city_id__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
