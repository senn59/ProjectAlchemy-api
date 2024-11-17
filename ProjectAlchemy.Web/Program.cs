using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Interfaces;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Persistence;
using ProjectAlchemy.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connString == null) throw new ArgumentNullException($"Connection string cannot be null");

builder.Services.AddScoped<AppDbContext>(_ => new AppDbContext(connString));
builder.Services.AddScoped<IIssueRepository>(s => new IssueRepository(s.GetRequiredService<AppDbContext>()));
builder.Services.AddScoped<IssueService>(s => new IssueService(s.GetRequiredService<IIssueRepository>()));


var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await db.Database.MigrateAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();