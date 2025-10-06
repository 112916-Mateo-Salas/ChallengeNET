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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //FLUENT API
            //Configuracion  para agregar Validaciones y Reglas para Usuario 
            modelBuilder.Entity<Usuario>(builder =>
            {
                builder.ToTable("Usuario");
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();
                builder.Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(250);
                builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(250);
                builder.Property(u => u.FechaCreacion)
                .HasColumnType("datetime(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .ValueGeneratedOnAdd();

                // Relación 1:1 con Domicilio
                builder.HasOne(u => u.Domicilio)            // Usuario tiene un domicilio
                       .WithOne(d => d.Usuario)             // Domicilio pertenece a un usuario
                       .HasForeignKey<Domicilio>(d => d.UsuarioId); // FK en Domicilio
            });

            //Configuracion  para agregar Validaciones y Reglas para Domicilio 
            modelBuilder.Entity<Domicilio>(builder =>
            {
                builder.ToTable("Domicilio");

                builder.HasKey(d => d.Id);

                builder.Property(d => d.Id)
                       .ValueGeneratedOnAdd();

                builder.Property(d => d.UsuarioId)
                       .IsRequired();

                builder.Property(d => d.FechaCreacion)
                       .HasColumnType("datetime(6)")
                       .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                       .ValueGeneratedOnAdd();
            });
        }


    }
}
