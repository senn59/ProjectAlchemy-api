using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Interfaces;
using ProjectAlchemy.Core.Services;
using ProjectAlchemy.Persistence;
using ProjectAlchemy.Persistence.Repositories;
using ProjectAlchemy.Web;
using ProjectAlchemy.Web.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();


var connString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connString == null)
{
    Console.Error.WriteLine("Connection string not properly configured");
    Environment.Exit(1);
}

var supabaseSettings = builder.Configuration.GetSection("Supabase").Get<SupabaseSettings>();
if (supabaseSettings == null || string.IsNullOrEmpty(supabaseSettings.Secret))
{
    Console.Error.WriteLine("Supabase settings are not properly configured");
    Environment.Exit(1);
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connString, ServerVersion.AutoDetect(connString));
});

builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<ILaneRepository, LaneRepository>();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<IssueService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<LaneService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.IncludeErrorDetails = true;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(supabaseSettings.Secret)
            ),
            ValidateIssuer = false,
            ValidateAudience = true,
            ValidAudience = "authenticated",
        };
    });

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

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();