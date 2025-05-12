using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SEBO.API.Data;
using SEBO.API.Domain.Entities.IdentityAggregate;
using SEBO.API.Repository.ProductAggregate;
using SEBO.API.Repository.IdentityAggregate;
using SEBO.API.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ItemRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<TransactionService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<SEBOContext>()
    .AddDefaultTokenProviders();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SEBOContext>(options => { options.UseSqlServer(connString); });

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Sebo", Version = "v1" });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
                "JWT Authorization Header\r\n\r\n" +
                "Informe seu token segundo o formato: 'Bearer [Token]'.\r\n\r\n" +
                "Exemplo: 'Bearer ABC123DFG456'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
     });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCustomIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
