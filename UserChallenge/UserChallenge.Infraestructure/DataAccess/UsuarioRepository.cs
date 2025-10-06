using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UserChallenge.Application.DTOs.Usuarios;
using UserChallenge.Application.Repositories;
using UserChallenge.Domain.Model;
using UserChallenge.Infraestructure.Context;

namespace UserChallenge.Infraestructure.DataAccess
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario> CreteUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task DeleteUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();  
        }

        public async Task<Usuario> GetById(int id)
        {
            return await _context.Usuarios.Include(u => u.Domicilio).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Usuario>> GetUsuariosByFilters(FiltersUsuario filtros)
        {
            var query = _context.Usuarios.Include(u => u.Domicilio).AsQueryable();

            //Controlo que los parametros que vienen no sean nulos para agregar
            if (!string.IsNullOrEmpty(filtros.Nombre))
                query = query.Where(u => u.Nombre.Contains(filtros.Nombre));

            if (!string.IsNullOrEmpty(filtros.Ciudad))
                query = query.Where(u => u.Domicilio != null && u.Domicilio.Ciudad.Contains(filtros.Ciudad));

            if (!string.IsNullOrEmpty(filtros.Provincia))
                query = query.Where(u => u.Domicilio != null && u.Domicilio.Provincia.Contains(filtros.Provincia));

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
