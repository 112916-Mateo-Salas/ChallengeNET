using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Application.DTOs.Domicilios;
using UserChallenge.Application.DTOs.Usuarios;
using UserChallenge.Application.Repositories;
using UserChallenge.Application.Services;
using UserChallenge.Domain.Model;

namespace UserChallenge.UnitTest.UsuarioTests
{
    public class UsuarioServiceTest
    {
        private readonly Mock<IUsuarioRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTest()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IMapper>();
            _usuarioService = new UsuarioService(_repositoryMock.Object, _mapperMock.Object);
        }

        // ------------------------
        // CreateUsuarioTest tests
        // ------------------------
        [Fact]
        public async Task CreateUsuarioTest_NullUser() {
            

            //Action
            await Assert.ThrowsAsync<ArgumentNullException>(() => _usuarioService.CreateUsuario(null));
            
        }

        [Fact]
        public async Task CreateUsuarioTest_Successfull()
        {
            RegisterUsuario register = new RegisterUsuario
            {
                Nombre = "Mateo",
                Email = "mateo@gmail.com"
            };

            Usuario usuario = GetUsuario();

            CreatedUsuarioDto createdUsuario = new CreatedUsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email
            };

            _mapperMock
                .Setup(m => m.Map<Usuario>(It.IsAny<RegisterUsuario>()))
                .Returns(usuario);
            _repositoryMock
                .Setup(r => r.CreteUsuario(It.IsAny<Usuario>()))
                .ReturnsAsync(usuario);

            _mapperMock
                .Setup(m => m.Map<CreatedUsuarioDto>(It.IsAny<Usuario>()))
                .Returns(createdUsuario);

            var result = await _usuarioService.CreateUsuario(register);

            Assert.NotNull(result);
            Assert.Equal(register.Nombre, result.Nombre);
        }

        // ------------------------
        // DeleteUsuario tests
        // ------------------------
        [Fact]
        public async Task DeleteUsuarioTest_Successfull()
        {
            int id = 1;
            Usuario usuario = GetUsuario();
            _repositoryMock
                .Setup(r => r.GetById(id))
                .ReturnsAsync(usuario);

            _repositoryMock
                .Setup(r => r.DeleteUsuario(It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);

            await _usuarioService.DeleteUsuario(id);

            _repositoryMock.Verify(r => r.DeleteUsuario(It.Is<Usuario>(u => u.Id == usuario.Id)), Times.Once);

        }

        [Fact]
        public async Task DeleteUsuarioTest_ExceptionNullUser()
        {
            int id = 1;
            _repositoryMock
                .Setup(r => r.GetById(id))
                .ReturnsAsync((Usuario)null);


            await Assert.ThrowsAsync<KeyNotFoundException>(() => _usuarioService.DeleteUsuario(id));

        }


        // ------------------------
        // GetUsuarioById tests
        // ------------------------
        [Fact]
        public async Task GetUsuarioByIdTest_Successfull()
        {
            // Arrange
            int id = 1;
            var usuario = GetUsuario();
            var usuarioDto = GetUsuarioDto();

            _repositoryMock
                .Setup(r => r.GetById(id))
                .ReturnsAsync(usuario);

            _mapperMock
                .Setup(m => m.Map<UsuarioDto>(usuario))
                .Returns(usuarioDto);

            // Action
            var result = await _usuarioService.GetUsuarioById(id);

            // Assert
            Assert.Same(usuarioDto, result);
            _repositoryMock.Verify(r => r.GetById(id), Times.Once);
            _mapperMock.Verify(m => m.Map<UsuarioDto>(usuario), Times.Once);
        }

        [Fact]
        public async Task GetUsuarioByIdTest_Exception_KeyNotFoundException()
        {
            // Arrange
            int id = 1;
            _repositoryMock
                .Setup(r => r.GetById(id))
                .ReturnsAsync((Usuario)null); 

            // Action & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _usuarioService.GetUsuarioById(id));
        }

        // ------------------------
        // UpdateUsuario tests
        // ------------------------
        [Fact]
        public async Task UpdateUsuarioTest_NullllUpdate_ArgumentNullException()
        {
            // Arrange
            int usuarioId = 1;
            UpdateUsuario updateUsuario = null;

            // Action & Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _usuarioService.UpdateUsuario(usuarioId, updateUsuario));
            
        }

        [Fact]
        public async Task UpdateUsuarioTest_Exception_KeyNotFoundException()
        {
            // Arrange
            int usuarioId = 1;
            var updateUsuario = new UpdateUsuario();

            _repositoryMock
                .Setup(r => r.GetById(usuarioId))
                .ReturnsAsync((Usuario)null);

            // Action & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _usuarioService.UpdateUsuario(usuarioId, updateUsuario));

           
        }

        [Fact]
        public async Task UpdateUsuarioTest_Successfull()
        {
            // Arrange
            int usuarioId = 1;
            var domicilioDto = GetDomicilioDto();
            var usuario = GetUsuario();
            var updateUsuario = GetUpdateUsuario();
            var usuarioUpdated = GetUsuarioUpdated();
            var domicilio = GetDomicilio();


            _repositoryMock
                .Setup(r => r.GetById(usuarioId))
                .ReturnsAsync(usuario);

            _mapperMock
                .Setup(m => m.Map(updateUsuario, usuario))
                .Returns(usuarioUpdated);

            _mapperMock
                .Setup(m => m.Map<Domicilio>(It.IsAny<DomicilioDto>()))
                .Returns(domicilio);

            _repositoryMock
                .Setup(r => r.UpdateAsync(usuarioUpdated))
                .Returns(Task.CompletedTask);

            // Action
            await _usuarioService.UpdateUsuario(usuarioId, updateUsuario);

            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Usuario>()), Times.Once);
        }

        //-------------------------
        //GetAllUsuariosByFilters tests
        //-------------------------
        [Fact]
        public async Task GetAllUsuariosByFiltersTest_Successfull()
        {
            // Arrange
            var filtros = new FiltersUsuario
            {
                Nombre = "Mateo"
            };
            var usuarios = new List<Usuario>(); 
            usuarios.Add(GetUsuario());
            var usuariosDto = new List<UsuarioDto>();
            usuariosDto.Add(GetUsuarioDto());

            _repositoryMock
                .Setup(r => r.GetUsuariosByFilters(filtros))
                .ReturnsAsync(usuarios);

            _mapperMock
                .Setup(m => m.Map<List<UsuarioDto>>(usuarios))
                .Returns(usuariosDto);

            // Act
            var result = await _usuarioService.GetAllUsuariosByFilters(filtros);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, usuariosDto);
            _repositoryMock.Verify(r => r.GetUsuariosByFilters(filtros), Times.Once);
        }

        [Fact]
        public async Task GetAllUsuariosByFiltersTest_ReturnsEmptyList()
        {
            // Arrange
            var filtros = new FiltersUsuario
            {
                Nombre = "Roman"
            };

            _repositoryMock
                .Setup(r => r.GetUsuariosByFilters(filtros))
                .ReturnsAsync((List<Usuario>)null);

            _mapperMock
                .Setup(m => m.Map<List<UsuarioDto>>(null))
                .Returns(new List<UsuarioDto>());

            // Act
            var result = await _usuarioService.GetAllUsuariosByFilters(filtros);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _repositoryMock.Verify(r => r.GetUsuariosByFilters(filtros), Times.Once);
        }
        

        [Fact]
        public async Task GetAllUsuariosByFilters_RepositoryThrows_WrapsInInvalidOperationException()
        {
            // Arrange
            var filtros = new FiltersUsuario
            {
                Nombre = "Mateo"
            };
            var inner = new Exception("error de conexion");

            _repositoryMock
                .Setup(r => r.GetUsuariosByFilters(filtros))
                .ThrowsAsync(inner);

            // Action & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _usuarioService.GetAllUsuariosByFilters(filtros));
            Assert.NotNull(ex.InnerException);
            Assert.Equal("error de conexion", ex.InnerException.Message);

            _repositoryMock.Verify(r => r.GetUsuariosByFilters(filtros), Times.Once);
            _mapperMock.Verify(m => m.Map<List<UsuarioDto>>(It.IsAny<List<Usuario>>()), Times.Never);
        }
    

        // ------------------------
        // Methods Get DTOs and Class tests
        // ------------------------
        private UsuarioDto GetUsuarioDto()
        {
            return new UsuarioDto
            {
                Nombre = "Mateo",
                Domicilio = new DomicilioDto
                {
                    Calle ="Publica",
                    Numero="7",
                    Provincia = "Córdoba",
                    Ciudad = "Córdoba"
                }                
            };
        }

        private Usuario GetUsuario()
        {
            return new Usuario
            {
                Nombre = "Mateo",
                Email = "mateo@gmail.com",
                FechaCreacion = DateTime.Parse("2025-10-06"),
                Id = 1

            };
        }

        private Usuario GetUsuarioUpdated()
        {
            return new Usuario
            {
                Nombre = "Mateo Salas",
                Email = "mateosalas@gmail.com",
                FechaCreacion = DateTime.Parse("2025-10-06"),
                Id = 1

            };
        }

        private Domicilio GetDomicilio()
        {
            return new Domicilio
            {
                Id = 1,
                Calle = "Publica",
                Numero = "7",
                Provincia = "Córdoba",
                Ciudad = "Córdoba",
                FechaCreacion = DateTime.Parse("2025-10-06")
            };
        }

        private DomicilioDto GetDomicilioDto()
        {
            return new DomicilioDto
            {
                
                Calle = "Publica",
                Numero = "7",
                Provincia = "Córdoba",
                Ciudad = "Córdoba"
            };
        }

        private UpdateUsuario GetUpdateUsuario()
        {
            return new UpdateUsuario
            {
                Nombre = "Mateo Salas",
                Email = "mateosalas@gmail.com",
                Domicilio = GetDomicilioDto(),
            };
        }

        
    }

    
}
