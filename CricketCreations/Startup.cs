using System;
using System.IO;
using CricketCreations.Interfaces;
using CricketCreations.Middleware;
using CricketCreations.Services;
using CricketCreationsDatabase;
using CricketCreationsRepository;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using RockLib.Logging;
//using VueCliMiddleware;
using RockLib.Logging.DependencyInjection;

namespace CricketCreations
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
            // string dbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            // services.AddDbContext<CricketCreationsContext>(opt => opt.UseSqlServer(dbConnectionString));
            services.AddControllers();
            services.AddTokenAuthentication(Configuration);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "clientapp";
                });
            }
            else
            {
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "clientapp/dist";
                });
            }

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddTransient<IBlogPostRepository, BlogPostRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPageRepository, PageRepository>();
            services.AddTransient<IBlogPostService, BlogPostService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IPageService, PageService>();
            services.AddTransient<IDatabaseManager, DatabaseManager>();
            services.AddSingleton<IImageService, ImageService>();

            string dir = Path.Join(Directory.GetCurrentDirectory(), "logs");
            Directory.CreateDirectory(dir);
            services.AddLogger().AddRollingFileLogProvider(opt =>
            {
                opt.File = Path.Join(dir, $"log.txt");
                opt.Level = LogLevel.Info;
                opt.Timeout = TimeSpan.FromSeconds(1);
                opt.MaxFileSizeKilobytes = 2048;
                opt.RolloverPeriod = RolloverPeriod.Daily;
            });

            services.AddSingleton<ILoggerService, LoggingService>();

            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services.AddDbContext<CricketCreationsContext>(x => x.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CricketCreationsContext cricketCreationsContext)
        {
            cricketCreationsContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }

            app.UseDefaultFiles();
            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("/index.html");
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "clientapp";
                }
                else
                {
                    spa.Options.SourcePath = "clientapp/dist";
                }

                //if (env.IsDevelopment())
                //{
                //    spa.UseVueCli(npmScript: "serve");
                //}
            });
        }
    }
}
