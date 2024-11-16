using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Text;
using WebApiUdApp.Repositories;
using WebApiUdApp.Services;
using WebApiUdApp.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<UsuarioRepositorio>();
builder.Services.AddScoped<PublicacionesRepositorio>();
builder.Services.AddScoped<PublicacionesService>();
builder.Services.AddScoped<UsuarioServicio>();
builder.Services.AddScoped<ReporteService>();
builder.Services.AddScoped<ReporteRepositorio>();
builder.Services.AddScoped<ModeradorService>();
builder.Services.AddScoped<ModeradorRepositorio>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<AdminRepositorio>();
builder.Services.AddTransient<SmtpCorreos>();
builder.Services.AddTransient<GenerarHtmlString>();
builder.Services.AddScoped<DatabaseConnection>();


builder.Services.AddControllers();

// Registro de DatabaseConnection para inyección de dependencias


// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:44322", "https://localhost:7023",
            "http://udapphosting-001-site1.ktempurl.com", "https://udapphosting-001-site1.ktempurl.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuración de JWT desde appsettings.json
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
    Debug.WriteLine("Key en Configuración JWT: " + jwtSettings["Key"]);
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = signingKey,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,

        // Configura "roleName" para que se trate como el rol del usuario
        RoleClaimType = "roleName"
    };
});

// Configuración de Swagger para JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebApiUdApp1",
        Version = "v1",
        Description = "Esta API proporciona acceso a diversas funcionalidades para la pagina web de UdApp."
    });

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
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
