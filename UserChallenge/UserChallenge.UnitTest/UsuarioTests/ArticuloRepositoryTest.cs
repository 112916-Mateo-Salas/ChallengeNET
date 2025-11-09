using Microsoft.EntityFrameworkCore;
//using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Domain.Model;
using UserChallenge.Infraestructure.Context;
using UserChallenge.Infraestructure.DataAccess;

namespace UserChallenge.UnitTest.UsuarioTests
{
    public class ArticuloRepositoryTest
    {

        private readonly AppDbContext _context;
        private readonly ArticuloRepository _articuloRepository;

        public ArticuloRepositoryTest()
        {
            // Base limpia para CADA test, porque el nombre cambia
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDB_{System.Guid.NewGuid()}")
                .Options;

            _context = new AppDbContext(options);
            _articuloRepository = new ArticuloRepository(_context);
        }

        private void SeedData(AppDbContext context)
        {
            var categoriaComputadoras = new Categoria { Id = 1, Nombre = "Computadoras", Descripcion = "" };
            var categoriaPerifericos = new Categoria { Id = 2, Nombre = "Perifericos", Descripcion = "" };

            var marcaDell = new Marca { Id = 1, Nombre = "Dell", Descripcion = "" };
            var marcaLogitech = new Marca { Id = 2, Nombre = "Logitech" , Descripcion = "" };

            context.Categorias.AddRange(categoriaComputadoras, categoriaPerifericos);
            context.Marcas.AddRange(marcaDell, marcaLogitech);

            context.Articulos.AddRange(
                new Articulo { Id = 1, Nombre = "Notebook Dell Inspiron", Precio = 800000, CategoriaId = 1, MarcaId = 1 },
                new Articulo { Id = 2, Nombre = "Monitor Dell 24 pulgadas", Precio = 150000, CategoriaId = 1, MarcaId = 1 },
                new Articulo { Id = 3, Nombre = "Teclado Logitech K380", Precio = 25000, CategoriaId = 2, MarcaId = 2 },
                new Articulo { Id = 4, Nombre = "Mouse Logitech G203", Precio = 18000, CategoriaId = 2, MarcaId = 2 }
            );

            context.SaveChanges();
        }

        [Fact]
        public async Task GetAllArticulosTest()
        {
            // Arrange
            
            _context.Articulos.AddRange(new List<Articulo>
        {
            new Articulo { Nombre = "Coca Cola", MarcaId = 1, Precio = 100 },
            new Articulo { Nombre = "Fanta", MarcaId = 1, Precio = 100 }
        });

            await _context.SaveChangesAsync();
            //var repo = new ArticuloRepository(context);

            // Act
            var result = await _articuloRepository.GetAllArticulos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Coca Cola", result[0].Nombre);
        }

        [Fact]
        public async Task GetArticuloByIdTest()
        {
            _context.Articulos.Add(new Articulo
            {
                Nombre = "Notebook HP Victus",
                MarcaId = 1,
                Precio = 1500,
                Id = 1,
                CategoriaId = 1
            });
            await _context.SaveChangesAsync();

            var result = await _articuloRepository.GetArticuloById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.MarcaId);
          
        }

        [Fact]
        public async Task GetArticuloTestMarca()
        {
            // Arrange
            _context.Marcas.AddRange(new List<Marca> { 
                new Marca {Id = 1, Nombre = "HP", Descripcion = "Computadoras HP", Activo = true },
                new Marca {Id = 2, Nombre = "Lenovo", Descripcion = "Computadoras Lenovo", Activo = true }
            });
            await _context.SaveChangesAsync();
            _context.Articulos.AddRange(new List<Articulo>
            {
                new Articulo {Id=1, Nombre = "Notebook HP Victus", MarcaId = 1 , Precio = 2100 },
                new Articulo {Id=2, Nombre = "Notebook Lenovo Legion", MarcaId = 2, Precio = 2500 }
            });

            await _context.SaveChangesAsync();

            var result = await _articuloRepository.GetArticulo(String.Empty, "Lenovo");
            Assert.NotNull(result);
            Assert.Equal("Lenovo", result.Marca.Nombre);
        }

        [Fact]
        public async Task GetArticuloTestNombre()
        {
            // Arrange
            _context.Marcas.AddRange(new List<Marca> {
                new Marca {Id = 1, Nombre = "HP", Descripcion = "Computadoras HP", Activo = true },
                new Marca {Id = 2, Nombre = "Lenovo", Descripcion = "Computadoras Lenovo", Activo = true }
            });
            await _context.SaveChangesAsync();
            _context.Articulos.AddRange(new List<Articulo>
            {
                new Articulo {Id=1, Nombre = "Notebook HP Victus", MarcaId = 1 , Precio = 2100 },
                new Articulo {Id=2, Nombre = "Notebook Lenovo Legion", MarcaId = 2, Precio = 2500 }
            });

            await _context.SaveChangesAsync();

            var result = await _articuloRepository.GetArticulo("Victus", String.Empty);
            Assert.NotNull(result);
            Assert.Equal("HP", result.Marca.Nombre);
        }


        [Fact]
        public async Task UpdateArticulo_DeberiaActualizarLosDatos()
        {
            // Arrange: agregamos un artículo inicial
            var articulo = new Articulo
            {
                Id = 1,
                Nombre = "Notebook Hp Victus",
                MarcaId = 1,
                Precio = 2100
            };

            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();

            // Modificamos el objeto en memoria
            articulo.Descripcion = "se agrega descripcion";
            articulo.Precio = 2500;

            // Act: llamamos al método a testear
            await _articuloRepository.UpdateArticulo(articulo);

            // Assert: volvemos a traer el artículo desde la base
            var articuloActualizado = await _context.Articulos.FindAsync(1);

            Assert.NotNull(articuloActualizado);
            Assert.Equal("se agrega descripcion", articuloActualizado.Descripcion);
            Assert.Equal(2500, articuloActualizado.Precio);
        }

        [Fact]
        public async Task GetArticulosByfilters_DeberiaFiltrarPorNombreCategoriaMarcaYPrecio()
        {
            // Arrange: cargar tablas relacionadas
            var categoriaComputadoras = new Categoria { Id = 1, Nombre = "Computadoras", Descripcion = "" };
            var categoriaCelulares = new Categoria { Id = 2, Nombre = "Celulares", Descripcion = "" };

            var marcaHP = new Marca { Id = 1, Nombre = "HP", Descripcion = "" };
            var marcaApple = new Marca { Id = 2, Nombre = "Apple" , Descripcion = "" };

            _context.Categorias.AddRange(categoriaComputadoras, categoriaCelulares);
            _context.Marcas.AddRange(marcaHP, marcaApple);
            await _context.SaveChangesAsync();

            _context.Articulos.AddRange(new List<Articulo>
            {
                new Articulo { Id = 1, Nombre = "Notebook HP Victus", MarcaId = 1, CategoriaId = 1, Precio = 2100 },
                new Articulo { Id = 2, Nombre = "Notebook HP Ieff", MarcaId = 1, CategoriaId = 1, Precio = 1800 },
                new Articulo { Id = 3, Nombre = "Iphone Apple 16 Pro Max", MarcaId = 2, CategoriaId = 2, Precio = 1100 },
                new Articulo { Id = 4, Nombre = "Iphone Apple 17 ", MarcaId = 2, CategoriaId = 2, Precio = 1000 }
            });

            await _context.SaveChangesAsync();

            // Act (filtramos por: nombre contiene "Coca", categoría bebidas, marca Coca, precio < 140)
            var result = await _articuloRepository.GetArticulosByfilters(
                nombre: "Notebook HP Victus",
                categoria: categoriaComputadoras,
                marca: "HP",
                precio: 2200
            );

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Notebook HP Victus", result.First().Nombre);
            Assert.Equal(1, result.First().CategoriaId);
            Assert.Equal(1, result.First().MarcaId);
        }


        [Fact]
        public async Task GetArticuloCount_SinFiltros_DeberiaDevolverTodos()
        {
            SeedData(_context);            

            var result = await _articuloRepository.GetArticuloCount(null, null, null, null);

            Assert.Equal(4, result);
        }

        [Fact]
        public async Task GetArticuloCount_FiltraPorCategoria()
        {
            SeedData(_context);

            var categoria = _context.Categorias.First(c => c.Id == 1);

            var result = await _articuloRepository.GetArticuloCount(categoria, null, null, null);

            Assert.Equal(2, result); // Notebook + Monitor
        }

        [Fact]
        public async Task GetArticuloCount_FiltraPorMarca()
        {
            SeedData(_context);

            var marca = _context.Marcas.First(m => m.Id == 2);

            var result = await _articuloRepository.GetArticuloCount(null, marca, null, null);

            Assert.Equal(2, result); // Teclado + Mouse
        }

        [Fact]
        public async Task GetArticuloCount_FiltraPorNombreParcial()
        {
            SeedData(_context);

            var result = await _articuloRepository.GetArticuloCount(null, null, "Dell", null);

            Assert.Equal(2, result); // Notebook + Monitor
        }

        [Fact]
        public async Task GetArticuloCount_FiltraPorPrecioMenor()
        {
            SeedData(_context);

            var result = await _articuloRepository.GetArticuloCount(null, null, null, 30000);

            Assert.Equal(2, result); // Teclado + Mouse
        }

        [Fact]
        public async Task GetArticuloCount_CombinacionDeFiltros()
        {
            SeedData(_context);

            var categoria = _context.Categorias.First(c => c.Id == 2);
            var marca = _context.Marcas.First(m => m.Id == 2);

            var result = await _articuloRepository.GetArticuloCount(categoria, marca, "Logitech", 30000);

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task GetArticulosByCategoria_DeberiaRetornarSoloLosDeLaCategoria()
        {
            // Arrange
            SeedData(_context);

            var categoria = _context.Categorias.First(c => c.Id == 1);

            // Act
            var result = await _articuloRepository.GetArticulosByCategoria(categoria);

            // Assert
            Assert.Equal(2, result.Count);                         // Deben ser solo los de categoría Computadoras
            Assert.All(result, a => Assert.Equal(1, a.CategoriaId)); // Todos deben pertenecer a esa categoría
            Assert.Contains(result, r => r.Nombre.Contains("Notebook"));
            Assert.Contains(result, r => r.Nombre.Contains("Monitor"));
        }

        [Fact]
        public async Task DeleteArticulo_DeberiaMarcarArticuloComoInactivo()
        {
            // Arrange           

            var articulo = new Articulo
            {
                Id = 1,
                Nombre = "Notebook Dell XPS",
                Precio = 950000,
                CategoriaId = 1,
                Activo = true 
            };
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();

            //Active
            articulo.Activo = false;

            await _articuloRepository.DeleteArticulo(articulo);

            // Assert
            var articuloDb = await _context.Articulos.FindAsync(1);

            Assert.NotNull(articuloDb);
            Assert.False(articuloDb.Activo); // Cambio de estado
        }
    }
}


