using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dinero> Dineros { get; set; }

    public virtual DbSet<TransaccionDetalle> TransaccionDetalles { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Fay-Lenovo;Database=cajero;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dinero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Dinero_Id");

            entity.ToTable("Dinero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Denominacion)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("denominacion");
            entity.Property(e => e.Existencia).HasColumnName("existencia");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<TransaccionDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TransaccionDetalle_Id");

            entity.ToTable("TransaccionDetalle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.DineroId).HasColumnName("dineroId");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.TransaccionId).HasColumnName("transaccionId");
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Transacciones_Id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estatus");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.Transaccion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("transaccion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
