using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Application.DTOs.Usuarios;
using UserChallenge.Domain.Model;

namespace UserChallenge.Application.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetById(int id);
        Task<List<Usuario>> GetUsuariosByFilters(FiltersUsuario filtros);

        Task<Usuario> CreteUsuario(Usuario usuario);

        Task UpdateAsync (Usuario usuario);
        Task DeleteUsuario(Usuario usuario);
    }
}
