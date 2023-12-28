using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Interfaces;
using ProEventos.Application.Services;
using ProEventos.Persistence;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ProEventosContext>(context => context.UseSqlite(connectionString));

builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // generic class
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IPalestranteRepository, PalestranteRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();

app.Run();
