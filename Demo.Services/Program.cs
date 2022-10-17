global using Microsoft.EntityFrameworkCore;
using Demo.Business.Data;
using Demo.Business.Implementation;
using Demo.Business.Repository;
using Demo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Scopes
builder.Services.AddScoped<IAddress, AddressRepository>();

// ADICIONA A CONEXÂO COM O BANCO
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Adicionando a classe backGround
builder.Services.AddHostedService<Demo.Services.BackGroundService.BackGround>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.ConfigureServices()
//{
//    Scopes.AddScopes(services);
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
