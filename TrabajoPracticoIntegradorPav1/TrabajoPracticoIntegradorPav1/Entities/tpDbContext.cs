using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TrabajoPracticoIntegradorPav1.Entities
{
    public partial class tpDbContext : DbContext
    {
        public tpDbContext()
            : base("name=TPS")
        {
        }

        public virtual DbSet<Barrio> Barrios { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Contacto> Contactos { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<FacturasDetalle> FacturasDetalles { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proyecto> Proyectos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barrio>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.cuit)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.razon_social)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.calle)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.numero)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Facturas)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contacto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Contacto>()
                .Property(e => e.apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Contacto>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Contacto>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Factura>()
                .Property(e => e.numero_factura)
                .IsUnicode(false);

            modelBuilder.Entity<FacturasDetalle>()
                .Property(e => e.precio)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Producto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.version)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.alcance)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.borrado)
                .IsFixedLength();

            modelBuilder.Entity<Usuario>()
                .Property(e => e.usuario1)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Facturas)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.id_usuario_creador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Proyectos)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.id_responsable);
        }
    }
}
