using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TzMazeScraper.Clients;
using TzMazeScraper.DbContext;
using TzMazeScraper.Services;
using TzMazeScraper.Settings;

namespace TzMazeScraper
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
            services.AddDbContext<TzMazeContext>(opt =>
                opt.UseSqlite("Data Source=TzMaze.db")
                   .EnableSensitiveDataLogging());

            services.Configure<PageSettings>(
                Configuration.GetSection(nameof(PageSettings)));
            services.Configure<ScraperSettings>(
                Configuration.GetSection(nameof(ScraperSettings)));

            services.AddSingleton<TzMazeClient>();
            services.AddSingleton<ScraperRunner>();
            services.AddTransient<ScraperService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
