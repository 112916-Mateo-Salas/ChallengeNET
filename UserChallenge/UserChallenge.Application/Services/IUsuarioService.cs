using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Application.DTOs.Usuarios;
using UserChallenge.Domain.Model;

namespace UserChallenge.Application.Services
{
    //Estos son mis Use Case - Caso de Uso
    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> GetAllUsuariosByFilters(FiltersUsuario filtros);

        Task<CreatedUsuarioDto> CreateUsuario(RegisterUsuario usuario);

        Task UpdateUsuario(int usuarioId, UpdateUsuario usuario);
        Task<UsuarioDto> GetUsuarioById(int id);
        Task DeleteUsuario(int id);
    }
}
