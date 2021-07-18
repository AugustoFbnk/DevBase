using DevBase.Domain.DTO.Cadastros;
using DevBase.Domain.Mapper.Cadastros;
using DevBase.Infra.Data.Contextos;
using DevBase.Infra.Data.Interfaces.Repositorios.Cadastros;
using DevBase.Infra.Data.Repositorios.Cadastros;
using DevBase.Services.Cadastros;
using DevBase.Services.Interfaces.Cadastros;
using DevBase.Services.Util.DtoValidator;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace DevBase.Api
{
    public class Startup
    {
        readonly string FullAccessLocal3000 = "_fullAccessLocal3000";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: FullAccessLocal3000,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                                  });
            });

            var server = Configuration["DbServer"];
            var port = Configuration["DbPort"];
            var user = Configuration["DbUser"];
            var password = Configuration["Password"];
            var database = Configuration["Database"];

            var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}";

            services.AddDbContext<DevBaseContext>(options =>
                 options.UseSqlServer(connectionString));

            services.AddControllers();

            ConfigurarRepositorios(services);
            ConfigurarBusinessServices(services);
            ConfigureValidators(services);

            services.AddAutoMapper(Assembly.Load(Assembly.GetAssembly(typeof(DesenvolvedorProfileMap)).FullName));
        }

        private void ConfigurarBusinessServices(IServiceCollection services)
        {
            services.AddScoped<IDesenvolvedorService, DesenvolvedorService>();
        }

        private void ConfigurarRepositorios(IServiceCollection services)
        {
            services.AddScoped<IDesenvolvedorRepositorio, DesenvolvedorRepositorio>();

        }

        public void ConfigureValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<DesenvolvedorDto>, DesenvolvedorDtoValidator>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(FullAccessLocal3000);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
