using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerkingCaloriesCalculator.Application;
using BurgerkingCaloriesCalculator.Application.UseCases;
using BurgerkingCaloriesCalculator.Domain.Repositories;
using BurgerkingCaloriesCalculator.Infrastructure.Context;
using BurgerkingCaloriesCalculator.Infrastructure.Repositories;
using BurgerkingCaloriesCalculator.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BurgerkingCaloriesCalculator.WebApp
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
            // options
            services.Configure<BurgerkingApiOptions>(Configuration.GetSection("BurgerKingAPI"));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("MenuDb"), builder => builder.MigrationsAssembly("burgerking-calories-calculator"));
            });
            
            // infrastructure dependencies
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddHttpClient<IProductRepository, BurgerKingApiProductRepository>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("BurgerKingAPI:BaseUrl"));
            });
            
            // application dependencies
            services.AddScoped<IFindAllProducts, ApplicationService>();
            services.AddScoped<ICreateMenu, ApplicationService>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, ApplicationDbContext dbContext)
        {
            logger.LogInformation("Configuration ---------");
            logger.LogInformation($"MenuDb = {Configuration.GetConnectionString("MenuDb")}");
            
            // This is only because it is required for the school submission
            // Usually i would use dotnet ef tooling (migrations - update) or create the tables by hand
            dbContext.Database.GetPendingMigrations();
            dbContext.Database.Migrate();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}