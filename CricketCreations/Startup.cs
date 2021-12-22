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
using VueCliMiddleware;

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
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
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
                    spa.Options.SourcePath = "dist";
                }

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }
            });
        }
    }
}
