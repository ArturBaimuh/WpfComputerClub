using Adaptor.repository;
using DataBaseComputerClub;
using Domain.data;
using Interactor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AspComputerClub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspComputerClub", Version = "v1" });
            });
            services.AddTransient<ComputerInteractor>();
            services.AddTransient<IComputerRepository, ComputerRepository>();
            services.AddTransient<PersonInteractor>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<RoomInteractor>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddDbContext<DBmonitoring>(
                option => option.UseSqlite(Configuration.GetConnectionString("MyConnection"))
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspGameclube v1"));
            }

            // Блокировка маршрута Swagger UI
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/swagger"))
                {
                    context.Response.StatusCode = 404;
                    return;
                }

                await next();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
