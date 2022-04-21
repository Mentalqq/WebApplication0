using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Data.SqlClient;
using System.IO;
using WebApplication1.Application.Options;
using WebApplication1.Data;
using WebApplication1.Infrastructure;
using WebApplication1.PipelineBehaviors;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DapperConnectionOptions>(Configuration.GetSection("ConnectionStrings"));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddControllers();
            services.AddCors();
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 3.1 Web API",
                    Description = "Authentication and Authorization in ASP.NET 3.1 with JWT and Swagger"
                });
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
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
                        new string[] {}
                    }
                });
        });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,
                            //Configuration["JWT: ValidIssuer"],

                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            //Configuration["JWT: ValidAudience"],

                            ValidateLifetime = true,

                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            //new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT: SecretKey"])),

                            ValidateIssuerSigningKey = true,
                        };
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                RunMigrations(app);
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MediatR Examples Api v1"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RunMigrations(IApplicationBuilder app)
        {
            try
            {
                string dbConnectionString = Configuration.GetConnectionString("SqlServerConnection");

                string projectPath = Directory.GetCurrentDirectory();
                var prePath = Path.Combine(projectPath, @"Migrations\Pre");
                var postPath = Path.Combine(projectPath, @"Migrations\Post");
                
                using var cnx = new SqlConnection(dbConnectionString);

                var evolvePreScripts = EvolveFactory.Create(cnx, prePath);
                evolvePreScripts.Migrate();

                var evolvePostScripts = EvolveFactory.Create(cnx, postPath);
                evolvePostScripts.Migrate();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }
    }
}
