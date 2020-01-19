using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComparatorApp.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ComparatorApp.API
{
    public class Startup
    {
        private string _sqliteConnectionStrings = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // using dotnet user-secrets
            // https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows#enable-secret-storage
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration["SqliteConnectionStrings"]));
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                // workaround loop error
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }); // required to use NewtonsoftJson instead of default System.Json

            services.AddAutoMapper(typeof(ItemRepository).Assembly);
            services.AddAutoMapper(typeof(StoreRepository).Assembly);
            services.AddAutoMapper(typeof(BrandRepository).Assembly);
            services.AddAutoMapper(typeof(BaseUnitRepository).Assembly);
            services.AddAutoMapper(typeof(ItemDetailRepository).Assembly);

            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBaseUnitRepository, BaseUnitRepository>();
            services.AddScoped<IItemDetailRepository, ItemDetailRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // redirects to Https
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
