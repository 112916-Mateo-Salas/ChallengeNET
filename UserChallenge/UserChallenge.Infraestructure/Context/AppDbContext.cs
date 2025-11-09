using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Domain.Model;

namespace UserChallenge.Infraestructure.Context
{
    [ExcludeFromCodeCoverage]
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Domicilio> Domicilios {  get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Marca> Marcas { get; set; }      
        public DbSet<Articulo> Articulos { get; set; } 

        public DbSet<Vehiculo> Vehiculos { get; set; }

        public DbSet<VehiculoImagen> VehiculoImagens { get; set; }  

        public DbSet<MarcaAuto> MarcaAutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(builder =>
            {
                builder.ToTable("Usuario");
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Id).ValueGeneratedOnAdd();

                builder.Property(u => u.Nombre)
                       .IsRequired()
                       .HasMaxLength(250);

                builder.Property(u => u.Email)
                       .IsRequired()
                       .HasMaxLength(250);

                // opcional: email unico
                builder.HasIndex(u => u.Email).IsUnique();

                //builder.Property(u => u.FechaCreacion)
                //       .HasColumnType("datetime(6)")
                //       .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                //       .ValueGeneratedOnAdd();

                builder.Property(u => u.FechaCreacion)
                       .HasColumnType("datetime2(6)")
                       .HasDefaultValueSql("SYSUTCDATETIME()")
                       .ValueGeneratedOnAdd();

                // Relación 1:1 con Domicilio
                builder.HasOne(u => u.Domicilio)
                       .WithOne(d => d.Usuario)
                       .HasForeignKey<Domicilio>(d => d.UsuarioId)
                       .OnDelete(DeleteBehavior.Cascade); // o Restrict según tu regla

                // Relación 1:N Rol -> Usuarios
                builder.HasOne(u => u.Rol)
                       .WithMany(r => r.Usuarios)
                       .HasForeignKey(u => u.RolId)   // <- CORREGIDO: usar la FK (RolId)
                       .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Domicilio>(builder =>
            {
                builder.ToTable("Domicilio");
                builder.HasKey(d => d.Id);
                builder.Property(d => d.Id).ValueGeneratedOnAdd();
                builder.Property(d => d.UsuarioId).IsRequired();

                // Forzar unicidad para 1:1
                builder.HasIndex(d => d.UsuarioId).IsUnique();

                //builder.Property(d => d.FechaCreacion)
                //       .HasColumnType("datetime(6)")
                //       .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                //       .ValueGeneratedOnAdd();

                builder.Property(d => d.FechaCreacion)
                       .HasColumnType("datetime2(6)")
                       .HasDefaultValueSql("SYSUTCDATETIME()")
                       .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Rol>(builder =>
            {
                builder.ToTable("Rol");
                builder.HasKey(r => r.Id);
                builder.Property(r => r.Id).ValueGeneratedOnAdd();

                builder.Property(r => r.Nombre)
                       .IsRequired()
                       .HasMaxLength(150);

                builder.HasIndex(r => r.Nombre).IsUnique();

                builder.Property(r => r.Activo)
                       .IsRequired()
                       .HasDefaultValue(true)    // usar true/false para bool
                       .ValueGeneratedOnAdd();

                //builder.Property(r => r.FechaCreacion)
                //       .HasColumnType("datetime(6)")
                //       .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                //       .ValueGeneratedOnAdd();

                builder.Property(r => r.FechaCreacion)
                       .HasColumnType("datetime2(6)")
                       .HasDefaultValueSql("SYSUTCDATETIME()")
                       .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Categoria>(builder =>
            {
                builder.ToTable("Categoria");
                builder.HasKey(c => c.Id);
                builder.Property(c => c.Id).ValueGeneratedOnAdd();

                builder.Property(c => c.Nombre)
                       .IsRequired()
                       .HasMaxLength(150);

                builder.HasIndex(c => c.Nombre).IsUnique();

                builder.Property(c => c.Activo)
                       .IsRequired()
                       .HasDefaultValue(true)
                       .ValueGeneratedOnAdd();

                //builder.Property(c => c.FechaCreacion)
                //       .HasColumnType("datetime(6)")
                //       .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                //       .ValueGeneratedOnAdd();
                builder.Property(c => c.FechaCreacion)
                       .HasColumnType("datetime2(6)")
                       .HasDefaultValueSql("SYSUTCDATETIME()")
                       .ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<Marca>(builder =>
            {
                builder.ToTable("Marca");
                builder.HasKey(m => m.Id);
                builder.Property(m => m.Id).ValueGeneratedOnAdd();

                builder.Property(m => m.Nombre)
                       .IsRequired()
                       .HasMaxLength(150);

                builder.Property(m => m.Activo)
                       .IsRequired()
                       .HasDefaultValue(true)
                       .ValueGeneratedOnAdd();

                //builder.Property(m => m.FechaCreacion)
                //       .HasColumnType("datetime(6)")
                //       .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                //       .ValueGeneratedOnAdd();

                builder.Property(m => m.FechaCreacion)
                       .HasColumnType("datetime2(6)")
                       .HasDefaultValueSql("SYSUTCDATETIME()")
                       .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Articulo>(builder =>
            {
                builder.ToTable("Articulo");
                builder.HasKey(a => a.Id);
                builder.Property(a => a.Id).ValueGeneratedOnAdd();

                builder.Property(a => a.Nombre)
                       .IsRequired()
                       .HasMaxLength(200);

                builder.Property(a => a.Descripcion)
                       .IsRequired()
                       .HasMaxLength(1000);

                builder.Property(a => a.Activo)
                       .IsRequired()
                       .HasDefaultValue(true)
                       .ValueGeneratedOnAdd();

                builder.HasOne(a => a.Marca)
                       .WithMany(m => m.Articulos)
                       .HasForeignKey(a => a.MarcaId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(a => a.Categoria)
                       .WithMany(c => c.Articulos)
                       .HasForeignKey(a => a.CategoriaId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(a => a.UsuarioCreador)
                       .WithMany(u => u.ArticulosCreados)
                       .HasForeignKey(a => a.UsuarioCreadorId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.Property(a => a.Precio)
                       .HasColumnType("decimal(18,2)")
                       .IsRequired();

                //builder.Property(a => a.FechaCreacion)
                //       .HasColumnType("datetime(6)")
                //       .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                //       .ValueGeneratedOnAdd();

                // SQL Server alternative:
                builder.Property(a => a.FechaCreacion)
                       .HasColumnType("datetime2(6)")
                       .HasDefaultValueSql("SYSUTCDATETIME()")
                       .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Vehiculo>(builder =>
            {
                builder.ToTable("Vehiculo");
                builder.HasKey(v => v.Id);
                builder.Property(v => v.Id).ValueGeneratedOnAdd();
                builder.Property(v => v.MarcaAutoId).IsRequired();
                builder.Property(v => v.Modelo).IsRequired().HasMaxLength(150);
                builder.Property(v => v.Activo).IsRequired().IsRequired().HasDefaultValue(true).ValueGeneratedOnAdd();
                builder.Property(v => v.Tipo).IsRequired().HasMaxLength(150);
                builder.Property(v => v.Patente).HasMaxLength(50).IsRequired();
                builder.Property(v => v.Version).HasMaxLength(100);
                builder.Property(v => v.Combustible).HasMaxLength(50);
                builder.Property(v => v.Transmision).HasMaxLength(50);
                builder.Property(v => v.Color).HasMaxLength(50);
                builder.Property(v => v.Traccion).HasMaxLength(50);
                builder.Property(v => v.Condicion).HasMaxLength(50);
                builder.Property(v => v.Estado).HasMaxLength(50);
                builder.Property(v => v.Precio).HasColumnType("decimal(18,2)").IsRequired();
                builder.Property(v => v.Descripcion).HasMaxLength(2000);

                builder.Property(v => v.FechaCreacion)
                       .HasColumnType("datetime2(6)")
                       .HasDefaultValueSql("SYSUTCDATETIME()")
                       .ValueGeneratedOnAdd();

                builder.Property(v => v.Activo)
                       .HasDefaultValue(true)
                       .ValueGeneratedOnAdd();

                // Relaciones
                builder.HasOne(v => v.MarcaAuto)
                       .WithMany(m => m.Vehiculos)
                       .HasForeignKey(v => v.MarcaAutoId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(v => v.UsuarioCreador)
                       .WithMany(u => u.VehiculosCreados)
                       .HasForeignKey(v => v.UsuarioCreadorId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasMany(v => v.Imagenes)
                       .WithOne(i => i.Vehiculo)
                       .HasForeignKey(i => i.VehiculoId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<VehiculoImagen>(builder =>
            {
                builder.ToTable("VehiculoImagen");
                builder.HasKey(i => i.Id);
                builder.Property(i => i.Id).ValueGeneratedOnAdd();
                builder.Property(i => i.Url).IsRequired().HasMaxLength(500);
                builder.Property(i => i.Portada).IsRequired();
                builder.Property(i => i.FechaCreacion)
                       .HasColumnType("datetime2(6)")
                       .HasDefaultValueSql("SYSUTCDATETIME()")
                       .ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<MarcaAuto>(builder => {
                builder.ToTable("MarcaAuto");
                builder.HasKey(ma => ma.Id);
                builder.Property(ma => ma.Id).IsRequired().ValueGeneratedOnAdd();
                builder.Property(ma => ma.Marca).IsRequired().HasMaxLength(150);
                builder.Property(ma => ma.Clase ).IsRequired();
                builder.Property(ma=> ma.Tipo ).IsRequired();
                builder.Property(ma=>ma.Activo).HasDefaultValue(true).ValueGeneratedOnAdd();
            });

        }



    }
}
