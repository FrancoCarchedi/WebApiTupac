using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiTupac.Data;
using WebApiTupac.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddScoped(typeof(IUsuarioRepository), typeof(UsuarioRepository));
builder.Services.AddScoped(typeof(ICarreraRepository), typeof(CarreraRepository));
builder.Services.AddScoped(typeof(IMateriaRepository), typeof(MateriaRepository));
//builder.Services.AddScoped(typeof(ICursadaRepository), typeof(CursadaRepository));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
