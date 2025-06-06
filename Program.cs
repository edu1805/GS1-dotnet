using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SOS_Natureza.Infrastructure.Mappings;
using SOS_natureza.Infrastructure.Repositories;
using System.Reflection;
using SOS_natureza.Infrastructure.Context;

namespace SOS_Natureza
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add controllers
            builder.Services.AddControllers();

            // Swagger com documenta��o via XML
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration["Swagger:Title"] ?? "SOS Natureza - API",
                    Version = "v1",
                    Description = "API para den�ncias de desastres ambientais."
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
                swagger.EnableAnnotations();
            });

            // Banco de dados Oracle via vari�vel de ambiente
            var oracleConnStr = Environment.GetEnvironmentVariable("ORACLE_CONNECTION_STRING");
            if (string.IsNullOrEmpty(oracleConnStr))
            {
                throw new InvalidOperationException("A vari�vel de ambiente 'ORACLE_CONNECTION_STRING' n�o foi encontrada.");
            }

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseOracle(oracleConnStr)
            );

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // Inje��o de depend�ncia dos reposit�rios
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IDenunciaRepository, DenunciaRepository>();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Middleware para desenvolvimento
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}