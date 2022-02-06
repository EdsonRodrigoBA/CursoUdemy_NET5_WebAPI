using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNetCore5.Models.Context;
using WebApiAspNetCore5.Business;
using WebApiAspNetCore5.Business.Implementations;
using WebApiAspNetCore5.Repository;

using Serilog;
using MySql.Data.MySqlClient;
using WebApiAspNetCore5.Repository.Generic;
using System.Net.Http.Headers;
using WebApiAspNetCore5.DB.HiperMidia.Filters;
using WebApiAspNetCore5.DB.HiperMidia.Enricher;
using Microsoft.AspNetCore.Rewrite;
using WebApiAspNetCore5.Services;
using WebApiAspNetCore5.Services.Implementations;
using WebApiAspNetCore5.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace WebApiAspNetCore5
{
    public class Startup
    {
        public IWebHostEnvironment _webHostEnvironment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            this._webHostEnvironment = webHostEnvironment;
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var tokenConfigurations = new TokenConfiguration();
            new Microsoft.Extensions.Options
                   .ConfigureFromConfigurationOptions<TokenConfiguration>(Configuration.GetSection("TokenConfiguration"))
                   .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);
            services.AddAuthentication(options =>
            {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfigurations.issuer,
                    ValidAudience = tokenConfigurations.audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.secrete))


                };
            });
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                            .RequireAuthenticatedUser().Build());
                auth.AddPolicy("DEVELOPPER", new AuthorizationPolicyBuilder()
                            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                            .RequireAuthenticatedUser().Build());
            });
            services.AddMvc();
            //services.AddMvc(options =>
            //{
            //    options.RespectBrowserAcceptHeader = true;
            //    //options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("aplication/xml").MediaType);
            //    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("aplication/json").MediaType);

            //}).AddXmlSerializerFormatters();
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();

            }));
            services.AddControllers();

            var connection = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connection));

            if (_webHostEnvironment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            services.AddScoped<IPersonBusiness, PersonBusinessmplementation>();

            services.AddScoped<IBooksBusiness, BooksBusinessmplementation>();
            services.AddScoped<IloginBusiness, loginBusinessImplementation>();


            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ITokenServices, TokenServices>();
            services.AddScoped<IusuarioRepository, UsuarioRepository>();


            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnrichers.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);

            services.AddApiVersioning();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Curso Rest Full .Net5 - Azure and Docker",

                    Version = "v1",
                    Description = "Api Rest Fulll",
                    Contact = new OpenApiContact()
                    {
                        Name = "Edson Rodrigo",
                        Url = new Uri("https://github.com/EdsonRodrigoBA/CursoUdemy_NET5_WebAPI"),
                        Email = "edsonrodrigoanalista@gmail.com"

                    }

                });
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso Rest Full.Net5 - Azure and Docker v1"));
            }

            app.UseHttpsRedirection();

            var options = new RewriteOptions();
            options.AddRedirect("^$", "swagger");
            app.UseRewriter(options);


            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");

            });
        }

        private void MigrateDatabase(string connection)
        {
            try
            {
                MySqlConnection evolveConnection = new MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<String> { "db/Migrations", "db/Datasets" },
                    IsEraseDisabled = true

                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("DataBase migration Failed" + ex);
                throw;
            }
        }
    }
}
