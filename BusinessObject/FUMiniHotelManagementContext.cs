﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BusinessObject
{
    public partial class FUMiniHotelManagementContext : DbContext
    {
        public FUMiniHotelManagementContext()
        {
        }

        public FUMiniHotelManagementContext(DbContextOptions<FUMiniHotelManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookingDetail> BookingDetails { get; set; } = null!;
        public virtual DbSet<BookingReservation> BookingReservations { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<RoomInformation> RoomInformations { get; set; } = null!;
        public virtual DbSet<RoomType> RoomTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return config["ConnectionStrings:DB"]!;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.HasKey(e => new { e.BookingReservationId, e.RoomId });

                entity.ToTable("BookingDetail");

                entity.Property(e => e.BookingReservationId).HasColumnName("BookingReservationID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.ActualPrice).HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.BookingReservation)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.BookingReservationId)
                    .HasConstraintName("FK_BookingDetail_BookingReservation");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_BookingDetail_RoomInformation");
            });

            modelBuilder.Entity<BookingReservation>(entity =>
            {
                entity.ToTable("BookingReservation");

                entity.Property(e => e.BookingReservationId)
                    .ValueGeneratedNever()
                    .HasColumnName("BookingReservationID");

                entity.Property(e => e.BookingDate).HasColumnType("date");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BookingReservations)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_BookingReservation_Customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.EmailAddress, "UQ__Customer__49A14740E19EB22A")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerBirthday).HasColumnType("date");

                entity.Property(e => e.CustomerFullName).HasMaxLength(50);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Telephone).HasMaxLength(12);
            });

            modelBuilder.Entity<RoomInformation>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.ToTable("RoomInformation");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.RoomDetailDescription).HasMaxLength(220);

                entity.Property(e => e.RoomNumber).HasMaxLength(50);

                entity.Property(e => e.RoomPricePerDay).HasColumnType("money");

                entity.Property(e => e.RoomTypeId).HasColumnName("RoomTypeID");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.RoomInformations)
                    .HasForeignKey(d => d.RoomTypeId)
                    .HasConstraintName("FK_RoomInformation_RoomType");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.ToTable("RoomType");

                entity.Property(e => e.RoomTypeId).HasColumnName("RoomTypeID");

                entity.Property(e => e.RoomTypeName).HasMaxLength(50);

                entity.Property(e => e.TypeDescription).HasMaxLength(250);

                entity.Property(e => e.TypeNote).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
