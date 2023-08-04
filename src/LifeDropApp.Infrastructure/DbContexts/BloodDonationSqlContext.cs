using System;
using System.Collections.Generic;
using LifeDropApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LifeDropApp.Infrastructure.DbContexts;

public partial class BloodDonationSqlContext : DbContext
{
    public BloodDonationSqlContext()
    {
    }

    public BloodDonationSqlContext(DbContextOptions<BloodDonationSqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; } = null!;

    public virtual DbSet<Admin> Admins { get; set; } = null!;

    public virtual DbSet<Donor> Donors { get; set; } = null!;

    public virtual DbSet<Hospital> Hospitals { get; set; } = null!;

    public virtual DbSet<NeedForBlood> NeedForBloods { get; set; } = null!;

    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Address__3214EC07D17F4492");

            entity.ToTable("Address");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admins__3214EC07993BC102");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Admins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Admins__UserId__3D5E1FD2");
        });

        modelBuilder.Entity<Donor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Donors__3214EC07AC2AA028");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BloodType)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Firstname)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Lastname)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.Address).WithMany(p => p.Donors)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Donors__AddressI__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.Donors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Donors__UserId__403A8C7D");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hospital__3214EC0700675D71");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Address).WithMany(p => p.Hospitals)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Hospitals__Addre__47DBAE45");

            entity.HasOne(d => d.User).WithMany(p => p.Hospitals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Hospitals__UserI__46E78A0C");
        });

        modelBuilder.Entity<NeedForBlood>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NeedForB__3214EC071915EB13");

            entity.ToTable("NeedForBlood");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BloodType)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.Hospital).WithMany(p => p.NeedForBloods)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NeedForBl__Hospi__4E88ABD4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0786FF4B50");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
