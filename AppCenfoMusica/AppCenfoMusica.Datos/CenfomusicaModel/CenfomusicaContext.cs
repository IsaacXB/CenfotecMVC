using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppCenfoMusica.Datos.CenfomusicaModel
{
    public partial class CenfomusicaContext : DbContext
    {
        public CenfomusicaContext()
        {
        }

        public CenfomusicaContext(DbContextOptions<CenfomusicaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<DetalleSolicitudCompra> DetalleSolicitudCompras { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<SolicitudCompra> SolicitudCompras { get; set; } = null!;
        public virtual DbSet<Vendedor> Vendedors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ISAAC-ALIENWARE\\SQLEXPRESS;Database=Cenfomusica;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Pkcliente);

                entity.ToTable("Cliente");

                entity.Property(e => e.Pkcliente).HasColumnName("PKCliente");

                entity.Property(e => e.EmlCorreo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FecNacimiento).HasColumnType("datetime");

                entity.Property(e => e.IdCedula)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IndContrasena)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.NomCliente)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomUsuario)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TelCliente)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DetalleSolicitudCompra>(entity =>
            {
                entity.HasKey(e => e.PkdetalleSolicitudCompra);

                entity.ToTable("DetalleSolicitudCompra");

                entity.Property(e => e.PkdetalleSolicitudCompra).HasColumnName("PKDetalleSolicitudCompra");

                entity.Property(e => e.Fkproducto).HasColumnName("FKProducto");

                entity.Property(e => e.FksolicitudCompra).HasColumnName("FKSolicitudCompra");

                entity.Property(e => e.MtoLinea).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.FkproductoNavigation)
                    .WithMany(p => p.DetalleSolicitudCompras)
                    .HasForeignKey(d => d.Fkproducto)
                    .HasConstraintName("FK_DetalleSolicitudCompra_Producto");

                entity.HasOne(d => d.FksolicitudCompraNavigation)
                    .WithMany(p => p.DetalleSolicitudCompras)
                    .HasForeignKey(d => d.FksolicitudCompra)
                    .HasConstraintName("FK_DetalleSolicitudCompra_SolicitudCompra");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Pkproducto);

                entity.ToTable("Producto");

                entity.Property(e => e.Pkproducto).HasColumnName("PKProducto");

                entity.Property(e => e.MtoPrecio).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.NomProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SolicitudCompra>(entity =>
            {
                entity.HasKey(e => e.PksolicitudCompra);

                entity.ToTable("SolicitudCompra");

                entity.Property(e => e.PksolicitudCompra).HasColumnName("PKSolicitudCompra");

                entity.Property(e => e.FecEntrega).HasColumnType("datetime");

                entity.Property(e => e.FecSolicitud).HasColumnType("datetime");

                entity.Property(e => e.Fkcliente).HasColumnName("FKCliente");

                entity.Property(e => e.Fkvendedor).HasColumnName("FKVendedor");

                entity.HasOne(d => d.FkclienteNavigation)
                    .WithMany(p => p.SolicitudCompras)
                    .HasForeignKey(d => d.Fkcliente)
                    .HasConstraintName("FK_SolicitudCompra_Cliente");

                entity.HasOne(d => d.FkvendedorNavigation)
                    .WithMany(p => p.SolicitudCompras)
                    .HasForeignKey(d => d.Fkvendedor)
                    .HasConstraintName("FK_SolicitudCompra_Vendedor");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasKey(e => e.Pkvendedor);

                entity.ToTable("Vendedor");

                entity.Property(e => e.Pkvendedor).HasColumnName("PKVendedor");

                entity.Property(e => e.IndCedula)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IndContrasena)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.NomUsuario)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NomVendedor)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
