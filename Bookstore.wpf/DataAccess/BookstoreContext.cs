using Bookstore.wpf.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Bookstore.wpf.DataAccess;

public partial class BookstoreContext : DbContext
{
    public BookstoreContext()
    {
    }

    public BookstoreContext(DbContextOptions<BookstoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Butiker> Butikers { get; set; }

    public virtual DbSet<Böcker> Böckers { get; set; }

    public virtual DbSet<Författare> Författares { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<KundRecensionsStatistik> KundRecensionsStatistiks { get; set; }

    public virtual DbSet<Kunder> Kunders { get; set; }

    public virtual DbSet<LagerSaldo> LagerSaldos { get; set; }

    public virtual DbSet<OrderHuvud> OrderHuvuds { get; set; }

    public virtual DbSet<OrderRader> OrderRaders { get; set; }

    public virtual DbSet<Recensioner> Recensioners { get; set; }

    public virtual DbSet<TitlarPerFörfattare> TitlarPerFörfattares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Made connection string into a secrets.json and removed it from OnConfiguring():
        var config = new ConfigurationBuilder().AddUserSecrets<BookstoreContext>().Build();
        var connectionString = config["ConnectionString"];
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Butiker>(entity =>
        {
            entity.HasKey(e => e.ButikId).HasName("PK__Butiker__B5D66BFADA1699C1");

            entity.ToTable("Butiker");

            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.Gatuadress).HasMaxLength(100);
            entity.Property(e => e.Land).HasMaxLength(50);
            entity.Property(e => e.Namn).HasMaxLength(50);
            entity.Property(e => e.Postnummer).HasMaxLength(10);
            entity.Property(e => e.Stad).HasMaxLength(50);
            entity.Property(e => e.Telefon).HasMaxLength(20);
        });

        modelBuilder.Entity<Böcker>(entity =>
        {
            entity.HasKey(e => e.Isbn13).HasName("PK__Böcker__3BF79E038F644602");

            entity.ToTable("Böcker");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.Pris).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Språk).HasMaxLength(50);
            entity.Property(e => e.Titel).HasMaxLength(200);

            entity.HasOne(d => d.Genre).WithMany(p => p.Böckers)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_Böcker_Genre");
        });

        modelBuilder.Entity<Författare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Författa__3214EC270F5C8D01");

            entity.ToTable("Författare");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Efternamn).HasMaxLength(20);
            entity.Property(e => e.Förnamn).HasMaxLength(20);

            entity.HasMany(d => d.Isbn13s).WithMany(p => p.Författares)
                .UsingEntity<Dictionary<string, object>>(
                    "FörfattareBok",
                    r => r.HasOne<Böcker>().WithMany()
                        .HasForeignKey("Isbn13")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FörfattareBok_Böcker"),
                    l => l.HasOne<Författare>().WithMany()
                        .HasForeignKey("FörfattareId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FörfattareBok_Författare"),
                    j =>
                    {
                        j.HasKey("FörfattareId", "Isbn13");
                        j.ToTable("FörfattareBok");
                        j.IndexerProperty<int>("FörfattareId").HasColumnName("FörfattareID");
                        j.IndexerProperty<string>("Isbn13")
                            .HasMaxLength(13)
                            .HasColumnName("ISBN13");
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genre__0385055ECDCCB7AA");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.Beskrivning).HasMaxLength(500);
            entity.Property(e => e.Namn).HasMaxLength(100);
        });

        modelBuilder.Entity<KundRecensionsStatistik>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("KundRecensionsStatistik");

            entity.Property(e => e.KundId).HasColumnName("KundID");
            entity.Property(e => e.Namn).HasMaxLength(101);
            entity.Property(e => e.RabattProcent).HasColumnType("numeric(2, 2)");
        });

        modelBuilder.Entity<Kunder>(entity =>
        {
            entity.HasKey(e => e.KundId).HasName("PK__Kunder__F2B5DEAC4D472EEB");

            entity.ToTable("Kunder");

            entity.HasIndex(e => e.Epost, "UQ_Kunder_Epost").IsUnique();

            entity.Property(e => e.KundId).HasColumnName("KundID");
            entity.Property(e => e.Efternamn).HasMaxLength(50);
            entity.Property(e => e.Epost).HasMaxLength(100);
            entity.Property(e => e.Förnamn).HasMaxLength(50);
            entity.Property(e => e.Gatuadress).HasMaxLength(100);
            entity.Property(e => e.Land).HasMaxLength(50);
            entity.Property(e => e.Postnummer).HasMaxLength(10);
            entity.Property(e => e.Stad).HasMaxLength(50);
            entity.Property(e => e.Telefon).HasMaxLength(20);
        });

        modelBuilder.Entity<LagerSaldo>(entity =>
        {
            entity.HasKey(e => new { e.ButikId, e.Isbn13 });

            entity.ToTable("LagerSaldo");

            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");

            entity.HasOne(d => d.Butik).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.ButikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LagerSaldo_Butiker");

            entity.HasOne(d => d.Isbn13Navigation).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LagerSaldo_Böcker");
        });

        modelBuilder.Entity<OrderHuvud>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Ordrar__C3905BAFFFAA25A2");

            entity.ToTable("OrderHuvud");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.KundId).HasColumnName("KundID");

            entity.HasOne(d => d.Butik).WithMany(p => p.OrderHuvuds)
                .HasForeignKey(d => d.ButikId)
                .HasConstraintName("FK_Ordrar_Butiker");

            entity.HasOne(d => d.Kund).WithMany(p => p.OrderHuvuds)
                .HasForeignKey(d => d.KundId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordrar_Kunder");
        });

        modelBuilder.Entity<OrderRader>(entity =>
        {
            entity.HasKey(e => e.OrderRadId).HasName("PK__OrderRad__C299EA2CFEAC6F61");

            entity.ToTable("OrderRader");

            entity.Property(e => e.OrderRadId).HasColumnName("OrderRadID");
            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Pris).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Isbn13Navigation).WithMany(p => p.OrderRaders)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderRader_Böcker");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderRaders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderRader_OrderHuvud");
        });

        modelBuilder.Entity<Recensioner>(entity =>
        {
            entity.HasKey(e => e.RecensionId).HasName("PK__Recensio__B8ADC8EB2AB6392A");

            entity.ToTable("Recensioner");

            entity.Property(e => e.RecensionId).HasColumnName("RecensionID");
            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            entity.Property(e => e.Kommentar).HasMaxLength(1000);
            entity.Property(e => e.KundId).HasColumnName("KundID");

            entity.HasOne(d => d.Isbn13Navigation).WithMany(p => p.Recensioners)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recensioner_Böcker");

            entity.HasOne(d => d.Kund).WithMany(p => p.Recensioners)
                .HasForeignKey(d => d.KundId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recensioner_Kunder");
        });

        //added when moving configuration for TitlarPerFörfattare from OnModelCreating() to TitlarPerFörfattareEntityTypeConfiguration.cs:
        new TitlarPerFörfattareEntityTypeConfiguration().Configure(modelBuilder.Entity<TitlarPerFörfattare>());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

// TODO: move configuration for all entities from OnModelCreating() to EntityTypeConfiguration.cs ?