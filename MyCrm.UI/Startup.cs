using System;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyCrm.Domain.Query.Dto.Auth;
using MyCrm.Domain.Repositories;
using MyCrm.Infrastructure;
using MyCrm.Infrastructure.Repositories;

namespace MyCrm.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JwtOptions jwtOptions = new JwtOptions();
            Configuration.GetSection("JwtOptions").Bind(jwtOptions);
            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = "Bearer";
                    option.DefaultScheme = "Bearer";
                    option.DefaultChallengeScheme = "Bearer";
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = jwtOptions.JwtIssuer,
                        ValidAudience = jwtOptions.JwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.JwtKey))
                    };

                    cfg.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["Authorization"];
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddSingleton(jwtOptions);

            services.AddDistributedRedisCache(r => { r.Configuration = Configuration["redis:connectionString"]; });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews();

            services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));

            var connectionString = Configuration.GetConnectionString("CrmDatabase");
            services.AddDbContext<CRMContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ContactsRepository>().As<IContactsRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<OrdersRepository>().As<IOrdersRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<RolesRepository>().As<IRolesRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TodosRepository>().As<ITodosRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TokenRepository>().As<ITokenRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<UsersRepository>().As<IUsersRepository>().InstancePerLifetimeScope();
            containerBuilder.ConfigureMediator();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
