
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserChallenge.API.Handlers;
using UserChallenge.Application.Mappings;
using UserChallenge.Application.Repositories;
using UserChallenge.Application.Services;
using UserChallenge.Application.Validators;
using UserChallenge.Infraestructure.Context;
using UserChallenge.Infraestructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

//Se configura el DBContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection"),
        b=> b.MigrationsAssembly("UserChallenge.Infraestructure") );
});
// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(UsuarioProfile).Assembly);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUsuarioValidators>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUsuarioValidators>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateDomicilioValidators>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    // Incluir los comentarios XML generados por el compilador
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Opcional: habilitar atributos de anotación como [SwaggerOperation]
    c.EnableAnnotations();
});

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("Development");

app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.Run();
