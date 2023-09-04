using DataAccess.Models;

using System.Text;
using BusinessLogic.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;

// Adicional Habilitar cors
string CORS_KEY = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Configuracion de CORS
builder.Services.AddCors(p => p.AddPolicy(CORS_KEY, builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

#region DbContext
builder.Services.AddDbContext<InwentContext>(options =>
{
    var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connectionStrings, builder =>
    {
        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});

#endregion


#region JWT Config
var tokenSettings = new TokenSettings();

builder.Configuration.GetSection("TokenSettings").Bind(tokenSettings);

// Pasa a cada controlador la configuracion
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidAudience = tokenSettings.AudienceToken,
            ValidIssuer = tokenSettings.IssuerToken,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(tokenSettings.SecurityKey)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };
    });

// Add JWT
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new()
    {
        Title = "Inwent API",
        Version = "V1",
    });


    option.AddSecurityDefinition("Bearer", new()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT"
    });

    option.AddSecurityRequirement(new()
    {
        {
            new()
            {
                Reference = new()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
#endregion

#region Servicios

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBaseService<BranchDto, BranchInputDto>, BranchService>();
#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cors
app.UseCors(CORS_KEY);

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
