using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiSDM.Models
{
    public partial class u204501959_SaborDeMexicoContext : DbContext
    {
        public u204501959_SaborDeMexicoContext()
        {
        }

        public u204501959_SaborDeMexicoContext(DbContextOptions<u204501959_SaborDeMexicoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adminitrador> Adminitrador { get; set; }
        public virtual DbSet<Carrito> Carrito { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<DetalleOrden> DetalleOrden { get; set; }
        public virtual DbSet<Orden> Orden { get; set; }
        public virtual DbSet<OrfertasDelDia> OrfertasDelDia { get; set; }
        public virtual DbSet<Presentacion> Presentacion { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Repartidor> Repartidor { get; set; }
        public virtual DbSet<Ruta> Ruta { get; set; }
        public virtual DbSet<Ubicacion> Ubicacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=217.21.76.51;database=u204501959_SaborDeMexico;user=u204501959_user;password=Rtx2080_", x => x.ServerVersion("10.5.15-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adminitrador>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Registro)
                    .HasColumnName("registro")
                    .HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasColumnName("user")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Carrito>(entity =>
            {
                entity.HasIndex(e => e.IdCliente)
                    .HasName("kf_idcliente_Carrito");

                entity.HasIndex(e => e.ProductoId)
                    .HasName("fk_Carrito_Producto1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Cantidad).HasColumnType("int(11)");

                entity.Property(e => e.IdCliente).HasColumnType("int(11)");

                entity.Property(e => e.Modificado).HasColumnType("datetime");

                entity.Property(e => e.Nota)
                    .HasColumnType("varchar(145)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ProductoId)
                    .HasColumnName("Producto_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Carrito)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("kf_idcliente_Carrito");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Carrito)
                    .HasForeignKey(d => d.ProductoId)
                    .HasConstraintName("fk_Carrito_Producto1");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Activo).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Activo).HasColumnType("int(11)");

                entity.Property(e => e.Contrasena)
                    .HasColumnType("varchar(245)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Correo)
                    .HasColumnType("varchar(145)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Modificado).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasColumnType("varchar(245)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Telefono)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Token)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<DetalleOrden>(entity =>
            {
                entity.HasKey(e => e.IdDetalleOrden)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdPresentacion)
                    .HasName("fk_DetalleOrden_Presentacion");

                entity.HasIndex(e => e.OrdenId)
                    .HasName("fk_DetalleOrden_Orden_idx");

                entity.HasIndex(e => e.ProductoId)
                    .HasName("fk_DetalleOrden_Producto1_idx");

                entity.Property(e => e.IdDetalleOrden)
                    .HasColumnName("idDetalleOrden")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cantidad).HasColumnType("int(11)");

                entity.Property(e => e.IdPresentacion)
                    .HasColumnName("Id_Presentacion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nota)
                    .HasColumnType("varchar(145)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.OrdenId)
                    .HasColumnName("Orden_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductoId)
                    .HasColumnName("Producto_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(20,2)");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.DetalleOrdenIdPresentacionNavigation)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DetalleOrden_Presentacion");

                entity.HasOne(d => d.Orden)
                    .WithMany(p => p.DetalleOrden)
                    .HasForeignKey(d => d.OrdenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DetalleOrden_Orden");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.DetalleOrdenProducto)
                    .HasForeignKey(d => d.ProductoId)
                    .HasConstraintName("fk_DetalleOrden_Producto1");
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasIndex(e => e.ClienteId)
                    .HasName("fk_Orden_Cliente1_idx");

                entity.HasIndex(e => e.RepartidorId)
                    .HasName("fk_Orden_Repartidor1_idx");

                entity.HasIndex(e => e.RutaId)
                    .HasName("fk_Orden_Ruta1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Activo).HasColumnType("int(11)");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClienteId)
                    .HasColumnName("Cliente_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CostoEnvio).HasColumnType("decimal(20,2)");

                entity.Property(e => e.Estatus).HasColumnType("int(11)");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Modifcado).HasColumnType("datetime");

                entity.Property(e => e.Notas)
                    .HasColumnType("varchar(245)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Ordencol)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.RepartidorId)
                    .HasColumnName("Repartidor_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RutaId)
                    .HasColumnName("Ruta_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TipoPago).HasColumnType("int(11)");

                entity.Property(e => e.Total).HasColumnType("decimal(20,2)");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Orden)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Orden_Cliente1");

                entity.HasOne(d => d.Repartidor)
                    .WithMany(p => p.Orden)
                    .HasForeignKey(d => d.RepartidorId)
                    .HasConstraintName("fk_Orden_Repartidor1");

                entity.HasOne(d => d.Ruta)
                    .WithMany(p => p.Orden)
                    .HasForeignKey(d => d.RutaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Orden_Ruta1");
            });

            modelBuilder.Entity<OrfertasDelDia>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Dia).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ProductoDelDiaId)
                    .HasColumnName("Producto_DelDia_Id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Presentacion>(entity =>
            {
                entity.HasIndex(e => e.IdProducto)
                    .HasName("fk_Presentacion_Producto");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.IdProducto)
                    .HasColumnName("Id_Producto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Medida)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Precentacion)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Precio).HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Presentacion)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Presentacion_Producto");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasIndex(e => e.Categoria)
                    .HasName("fk_categoria_Producto");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Activo).HasColumnType("int(11)");

                entity.Property(e => e.Categoria).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Modificado).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.CategoriaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.Categoria)
                    .HasConstraintName("fk_categoria_Producto");
            });

            modelBuilder.Entity<Repartidor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Activo).HasColumnType("int(11)");

                entity.Property(e => e.Modificado).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Pin)
                    .IsRequired()
                    .HasColumnName("pin")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Ruta>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Direccion)
                    .HasColumnType("varchar(345)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Google)
                    .HasColumnType("varchar(545)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Lat)
                    .HasColumnName("lat")
                    .HasColumnType("decimal(12,8)");

                entity.Property(e => e.Lon).HasColumnType("decimal(12,8)");
            });

            modelBuilder.Entity<Ubicacion>(entity =>
            {
                entity.HasIndex(e => e.ClienteId)
                    .HasName("fk_Ubicacion_Cliente1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClienteId)
                    .HasColumnName("Cliente_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Direccion)
                    .HasColumnType("varchar(545)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Lat)
                    .HasColumnName("lat")
                    .HasColumnType("decimal(12,8)");

                entity.Property(e => e.Lon)
                    .HasColumnName("lon")
                    .HasColumnType("decimal(12,8)");

                entity.Property(e => e.Nota)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Ubicacion)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ubicacion_Cliente1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
