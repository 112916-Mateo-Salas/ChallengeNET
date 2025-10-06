using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserChallenge.API.Handlers;

namespace UserChallenge.UnitTest.UsuarioTests
{
    public class ExceptionHandlerTest
    {
        private readonly Mock<ILogger<ExceptionHandlerMiddleware>> _loggerMock;

        public ExceptionHandlerTest()
        {
            _loggerMock = new Mock<ILogger<ExceptionHandlerMiddleware>>();
        }

        private ExceptionHandler CreateMiddleware(RequestDelegate next)
        {
            return new ExceptionHandler(next, _loggerMock.Object);
        }

        private HttpContext CreateContext()
        {
            var context = new DefaultHttpContext();
            // Usamos un memory stream para leer lo que escriba el middleware
            context.Response.Body = new MemoryStream();
            return context;
        }

        private async Task<string> ReadResponseBodyAsync(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }

        [Fact]
        public async Task Test_InvokeAsync_KeyNotFoundException_Returns404()
        {
            // Arrange
            var exception = new KeyNotFoundException("no existe el registro con id");
            RequestDelegate next = ctx => throw exception;
            var middleware = CreateMiddleware(next);
            var context = CreateContext();

            // Action
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, context.Response.StatusCode);
            var body = await ReadResponseBodyAsync(context);
            var doc = JsonDocument.Parse(body);
            var root = doc.RootElement;
            Assert.False(root.GetProperty("success").GetBoolean());
            Assert.Equal("no existe el registro con id", root.GetProperty("message").GetString());
            Assert.True(root.GetProperty("detail").ValueKind == JsonValueKind.Null || string.IsNullOrEmpty(root.GetProperty("detail").GetString()));
            
        }

        [Fact]
        public async Task Test_InvokeAsync_ArgumentNullException_Returns400()
        {
            // Arrange
            var exception = new ArgumentNullException("param");
            RequestDelegate next = ctx => throw exception;
            var middleware = CreateMiddleware(next);
            var context = CreateContext();

            // Action
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);
            var body = await ReadResponseBodyAsync(context);
            var doc = JsonDocument.Parse(body);
            var root = doc.RootElement;
            Assert.False(root.GetProperty("success").GetBoolean());
            Assert.Equal(exception.Message, root.GetProperty("message").GetString());
            
        }

        [Fact]
        public async Task Test_InvokeAsync_GenericException_Returns500()
        {
            // Arrange
            var inner = new Exception("Ha ocurrido un error");
            var exception = new InvalidOperationException("failed", inner);
            RequestDelegate next = ctx => throw exception;
            var middleware = CreateMiddleware(next);
            var context = CreateContext();

            // Action
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
            var body = await ReadResponseBodyAsync(context);
            var doc = JsonDocument.Parse(body);
            var root = doc.RootElement;
            Assert.False(root.GetProperty("success").GetBoolean());
            Assert.Equal("failed", root.GetProperty("message").GetString());
            Assert.Equal("Ha ocurrido un error", root.GetProperty("detail").GetString());
            
        }
    }
}
