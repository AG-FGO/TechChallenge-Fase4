using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TechChallenge_Application.Interfaces;
using TechChallenge_Application.Services.Acao;
using TechChallenge_Application.Services.Carteira;
using TechChallenge_Application.Services.ServiceBus;
using TechChallenge_Application.Services.Usuario;
using TechChallenge_Fase1.Logging;
using TechChallenge_Fase1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var configuration = builder.Configuration;
var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Secret"));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FiapStore", Version = "v1" });
    var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
    c.IncludeXmlComments(xmlpath);

    c.AddSecurityDefinition("Bearear", new OpenApiSecurityScheme
    {
        Description = "Autenticação baseada em Json Web Token (JWT)",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearear",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<TechChallenge_Data.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:ConnectionString")));

builder.Services.AddScoped<IUsuarioUseCase, UsuarioUseCase>();
builder.Services.AddScoped<TechChallenge_Domain.Interfaces.IUsuarioRepository, TechChallenge_Data.Repositories.UsuarioRepository>();
builder.Services.AddScoped<ICarteiraUseCase, CarteiraUseCase>();
builder.Services.AddScoped<TechChallenge_Domain.Interfaces.ICarteiraRepository, TechChallenge_Data.Repositories.CarteiraRepository>();
builder.Services.AddScoped<IAcaoUseCase, AcaoUseCase>();
builder.Services.AddScoped< TechChallenge_Domain.Interfaces.IAcaoRepository, TechChallenge_Data.Repositories.AcaoRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IServiceBusUseCase, ServiceBusEnvioConfiguration>();

/*builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAcaoRepository, AcaoRepository>();
builder.Services.AddScoped<ICarteiraRepository, CarteiraRepository>();
//builder.Services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<IServiceBusRepository, EnvioConfiguration>();*/

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

builder.Services.AddAuthentication(authService => {
    authService.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authService.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwtConfig => {
    jwtConfig.RequireHttpsMetadata = false;
    jwtConfig.SaveToken = true;
    jwtConfig.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var servidor = configuration.GetSection("MassTransitAzure")["Conexao"] ?? string.Empty;
builder.Services.AddMassTransit(x =>
{
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(servidor);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
        });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

 app.Run();
