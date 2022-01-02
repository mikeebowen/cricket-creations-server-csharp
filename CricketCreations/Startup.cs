using System;
using System.IO;
using CricketCreations.Interfaces;
using CricketCreations.Middleware;
using CricketCreations.Services;
using CricketCreationsRepository;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                    configuration.RootPath = "ClientApp";
                });
            }
            else
            {
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp/dist";
                });
            }

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddSingleton<IBlogPostRepository, BlogPostRepository>();
            services.AddSingleton<ITagRepository, TagRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPageRepository, PageRepository>();
            services.AddSingleton<IBlogPostService, BlogPostService>();
            services.AddSingleton<ITagService, TagService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<IPageService, PageService>();
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "ClientApp";
                }
                else
                {
                    spa.Options.SourcePath = "ClientApp/dist";
                }

                //if (env.IsDevelopment())
                //{
                //    spa.UseVueCli(npmScript: "serve");
                //}
            });
        }
    }
}
