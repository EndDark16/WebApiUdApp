using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using WebApiUdApp.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<PublicacionesRepositorio>();
builder.Services.AddTransient<UsuarioServicio>();
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de JWT desde appsettings.json
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
    Debug.WriteLine("Key en Configuración JWT: " + jwtSettings["Key"]);
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

    options.RequireHttpsMetadata = false; // Ajusta esto según tu entorno (producción/ desarrollo)

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Deshabilita la validación del emisor
        ValidateAudience = false, // Deshabilita la validación de la audiencia
        IssuerSigningKey = signingKey, // La clave de firma que se usó para crear el token
        ValidateLifetime = true, // Valida que el token no esté caducado
        ClockSkew = TimeSpan.Zero // El tiempo de tolerancia para la caducidad del token
    };

});

// Configurar Swagger para usar el token JWT en la autenticación
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebApiUdApp1",
        Version = "v1",
        Description = "Esta API proporciona acceso a diversas funcionalidades para la pagina web de UdApp." // Descripción añadida
    });

    // Configuración para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. \r\n\r\n " +
                      "Escriba 'Bearer' [espacio] y luego su token en el campo de texto. \r\n\r\n" +
                      "Ejemplo: 'Bearer olabolaesteesmitoken1234'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
