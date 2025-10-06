using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.API.Controllers;
using UserChallenge.Application.DTOs.Domicilios;
using UserChallenge.Application.DTOs.Usuarios;
using UserChallenge.Application.Services;
using UserChallenge.Domain.Model;

namespace UserChallenge.UnitTest.UsuarioTests
{
    
    
        public class UsuarioControllerTests
        {
            private readonly Mock<IUsuarioService> _serviceMock;
            private readonly UsuarioController _controller;

            public UsuarioControllerTests()
            {
                _serviceMock = new Mock<IUsuarioService>();
                _controller = new UsuarioController(_serviceMock.Object);
            }

            [Fact]
            public async Task GetUsuarioByIdTest_Successfull()
            {
                // Arrange
                int id = 1;
                var usuarioDto = GetUsuarioDto();
                _serviceMock.Setup(s => s.GetUsuarioById(id)).ReturnsAsync(usuarioDto);

                // Act
                var actionResult = await _controller.GetUsuarioById(id);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                Assert.Same(usuarioDto, okResult.Value);
                _serviceMock.Verify(s => s.GetUsuarioById(id), Times.Once);
            }

            [Fact]
            public async Task GetUsuariosByFiltersTest_Successfull()
            {
                // Arrange
                var filtros = new FiltersUsuario { Nombre = "Ana" };
                var usuarios = new List<UsuarioDto>
                    {
                        GetUsuarioDto()
                    };

                _serviceMock.Setup(s => s.GetAllUsuariosByFilters(filtros)).ReturnsAsync(usuarios);

                // Action
                var actionResult = await _controller.GetUsuariosByFilters(filtros);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                Assert.Same(usuarios, okResult.Value);
                _serviceMock.Verify(s => s.GetAllUsuariosByFilters(filtros), Times.Once);
            }

            [Fact]
            public async Task CreateUsuarioTest_Successfull()
            {
                // Arrange
                var register = new RegisterUsuario
                {
                    Nombre = "Mateo Salas",
                    Email = "mateosalas@gmail.com"
                };
                var created = new CreatedUsuarioDto 
                {
                    Id = 1,
                    Nombre = "Mateo Salas",
                    Email = "mateosalas@gmail.com",
                    FechaCreacion = DateTime.Parse("2025-10-06")
                };

                _serviceMock.Setup(s => s.CreateUsuario(register)).ReturnsAsync(created);

                // Action
                var actionResult = await _controller.CreateUsuario(register);

                // Assert
                var createdAt = Assert.IsType<CreatedAtActionResult>(actionResult);
                Assert.Equal(nameof(UsuarioController.GetUsuarioById), createdAt.ActionName);
                Assert.NotNull(createdAt.RouteValues);
                Assert.True(createdAt.RouteValues.ContainsKey("id"));
                Assert.Equal(created.Id, createdAt.RouteValues["id"]);

                _serviceMock.Verify(s => s.CreateUsuario(register), Times.Once);
            }

            [Fact]
            public async Task UpdateUsuarioTest_ReturnsNoContent()
            {
                // Arrange
                int id = 1;
                var usuarioUpdate = GetUpdateUsuario();
                _serviceMock.Setup(s => s.UpdateUsuario(id, usuarioUpdate)).Returns(Task.CompletedTask);

                // Action
                var result = await _controller.UpdateUsuario(id, usuarioUpdate);

                // Assert
                Assert.IsType<NoContentResult>(result);
                _serviceMock.Verify(s => s.UpdateUsuario(id, usuarioUpdate), Times.Once);
            }

            [Fact]
            public async Task DeleteUsuarioTest_ReturnsNoContent()
            {
                // Arrange
                int id = 1;
                _serviceMock.Setup(s => s.DeleteUsuario(id)).Returns(Task.CompletedTask);

                // Action
                var result = await _controller.DeleteUsuario(id);

                // Assert
                Assert.IsType<NoContentResult>(result);
                _serviceMock.Verify(s => s.DeleteUsuario(id), Times.Once);
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
                    Calle = "Publica",
                    Numero = "7",
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
