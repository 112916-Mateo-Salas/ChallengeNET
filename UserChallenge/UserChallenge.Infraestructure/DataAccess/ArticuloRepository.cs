using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Application.Repositories;
using UserChallenge.Domain.Model;
using UserChallenge.Infraestructure.Context;

namespace UserChallenge.Infraestructure.DataAccess
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly AppDbContext _context;

        public ArticuloRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task DeleteArticulo(Articulo articulo)
        {
            _context.Articulos.Update(articulo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Articulo>> GetAllArticulos()
        {
           return await _context.Articulos.Where(a => a.Activo == true).ToListAsync();            
        }

        public async Task<Articulo> GetArticulo(string nombre, string marca)
        {
            IQueryable<Articulo> query = _context.Articulos;
            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(a => a.Nombre.Contains(nombre));

            if (!string.IsNullOrEmpty(marca))
                query = query.Where(a => a.Marca.Nombre.Contains(marca));
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Articulo> GetArticuloById(int id)
        {
            return await _context.Articulos.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> GetArticuloCount(Categoria categoria, Marca marca, string nombre, decimal? precio)
        {
            IQueryable<Articulo> query = _context.Articulos;

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(a => a.Nombre.Contains(nombre));

            if (categoria != null)
                query = query.Where(a => a.CategoriaId == categoria.Id);

            if (marca != null)
                query = query.Where(a => a.MarcaId == marca.Id);

            if (precio.HasValue)
                query = query.Where(a => a.Precio < precio.Value);

            return await query.CountAsync();
        }

        public async Task<List<Articulo>> GetArticulosByCategoria(Categoria categoria)
        {
            return await _context.Articulos.Include(a => a.Categoria).Where(a => a.CategoriaId == categoria.Id).ToListAsync();
        }

        public async Task<List<Articulo>> GetArticulosByfilters(string nombre, Categoria categoria, string marca, decimal? precio)
        {
            IQueryable<Articulo> query = _context.Articulos;

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(a => a.Nombre.Contains(nombre));

            if (categoria != null)
                query = query.Where(a => a.CategoriaId == categoria.Id);

            if (!string.IsNullOrWhiteSpace(marca))
                query = query.Where(a => a.Marca.Nombre.Contains(marca));

            if (precio.HasValue)
                query = query.Where(a => a.Precio < precio.Value);

            return await query.ToListAsync();
        }

        public async Task UpdateArticulo(Articulo articulo)
        {
            _context.Articulos.Update(articulo);
            await _context.SaveChangesAsync();
        }
    }
}
