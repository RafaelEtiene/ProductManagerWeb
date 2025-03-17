using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProductManager.Data;
using ProductManager.Data.Migrations;
using ProductManager.Models.Entities;
using ProductManager.Models.Validator;
using ProductManager.Repositories;
using ProductManager.Services;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(runner => runner
        .AddPostgres() // Usando PostgreSQL como banco de dados
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection")) // Connection string do PostgreSQL
        .ScanIn(typeof(CreateProdutoTable).Assembly).For.Migrations() // Escaneia a assembly para migrações
    )
    .AddLogging(logging => logging.AddConsole()); // Adiciona o log para ver o progresso das migrações no console


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Configura o MVC com suporte para FluentValidation
builder.Services.AddControllersWithViews()
        .AddFluentValidation(config =>
        {
            config.RegisterValidatorsFromAssemblyContaining<Program>(); // Ou de algum assembly específico
        });

builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddScoped<IValidator<Produto>, ProdutoValidator>();

var cultureInfo = new CultureInfo("pt-BR");

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produto}/{action=Index}/{id?}");
app.Run();
