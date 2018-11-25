namespace GroupProjectBackend
{
    using Config;
    using Models.DB;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;

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
            IConfigProvider config = new ConfigProvider(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            //DB and identity
            services.AddDbContext<GroupProjectDbContext>(opt => opt.UseSqlServer(config.ConnectionString));
            System.Diagnostics.Trace.WriteLine("AAAAAAAAAAAAAAAA");
            System.Diagnostics.Trace.WriteLine(config.ConnectionString);
            System.Diagnostics.Trace.WriteLine("AAAAAAAAAAAAAAAA");
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<GroupProjectDbContext>()
                .AddDefaultTokenProviders();
            services.BuildServiceProvider().GetService<GroupProjectDbContext>().Database.Migrate();

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.User.RequireUniqueEmail = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
            });

            //Singletons
            services.AddSingleton<IConfigProvider>(config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, GroupProjectDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

        }
    }
}
