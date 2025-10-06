using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Application.DTOs.Usuarios;
using UserChallenge.Application.Mappings;
using UserChallenge.Application.Repositories;
using UserChallenge.Domain.Model;

namespace UserChallenge.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<CreatedUsuarioDto> CreateUsuario(RegisterUsuario register)
        {
            if(register == null)
                throw new ArgumentNullException(nameof(register), "Los datos del usuario a registrar no pueden ser nulos");
            try
            {
                Usuario newUsuario = _mapper.Map<Usuario>(register);
                newUsuario.Domicilio = _mapper.Map<Domicilio>(register.Domicilio);
                var usuario = await _usuarioRepository.CreteUsuario(newUsuario);
                if (usuario.Id == null)
                    throw new InvalidOperationException("No se pudo crear el usuario");
                return _mapper.Map<CreatedUsuarioDto>(usuario);
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }

        

        public async Task DeleteUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);
            if (usuario == null) 
                throw new KeyNotFoundException($"No se ha encontrado el usuario con id {id}"); 

            await _usuarioRepository.DeleteUsuario(usuario);
        }

        public async Task<List<UsuarioDto>> GetAllUsuariosByFilters(FiltersUsuario filtros)
        {
            try
            {
                List<Usuario> usuarios = await _usuarioRepository.GetUsuariosByFilters(filtros);

                return _mapper.Map<List<UsuarioDto>>(usuarios);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al tratar de traer los usuarios ", ex);
            }
            
        }

        public async Task<UsuarioDto> GetUsuarioById(int id)
        {
            Usuario usuario = await _usuarioRepository.GetById(id);
            if (usuario == null)
                throw new KeyNotFoundException($"No se ha encontrado el usuario con id {id}");
            return _mapper.Map<UsuarioDto>(usuario); ;
        }

        public async Task UpdateUsuario(int usuarioId, UpdateUsuario updateUsuario )
        {
            if (updateUsuario == null)
                throw new ArgumentNullException(nameof(updateUsuario));
            try
            {
                var usuario = await _usuarioRepository.GetById(usuarioId);
                if (usuario == null)
                    throw new KeyNotFoundException($"No se ha encontrado el usuario con id {usuarioId}");

                // Mapeo los cambios del DTO al usuario existente
                _mapper.Map(updateUsuario, usuario);

                // Si se envió domicilio y no existía, se crea uno nuevo
                if (updateUsuario.Domicilio != null && usuario.Domicilio == null)
                {
                    usuario.Domicilio = _mapper.Map<Domicilio>(updateUsuario.Domicilio);
                }

                await _usuarioRepository.UpdateAsync(usuario);
            }
            catch (Exception ex) 
            {
                throw;
            }
            
        }
    }
}
