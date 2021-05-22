using Almocherifado.Application;
using Almocherifado.core.Services;
using Almocherifado.InfraEstrutura;
using Almocherifado.InfraEstrutura.Repositorios;
using Almocherifado.UI.Areas.Identity;
using Almocherifado.UI.Data;
using Almocherifado.UI.Helpers.FileHelpers;
using Append.Blazor.Printing;
using AutoMapper;
using BlazorDownloadFile;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Almocherifado.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => 
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password = new PasswordOptions()
                {
                    RequireDigit = false,
                    RequiredLength = 0,
                    RequiredUniqueChars = 0,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                };
                
            } )
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            


            services.AddBlazorise(options => options.ChangeTextOnKeyPress = false )
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            services.AddDbContext<AlmocherifadoContext>(x => 
                x.UseSqlite(@"Data Source = almocherifado.db")
                .UseLazyLoadingProxies());
            
            services.AddBlazorDownloadFile();
            services.AddScoped<IPrintingService, PrintingService>();


            services.Scan(scan => scan.FromAssembliesOf(typeof(FuncionariosRepository))
                .AddClasses()
                .AsImplementedInterfaces()
            );

            //application
            services.Scan(scan => scan.FromAssembliesOf(typeof(FerramentasService), typeof(TermoManager), typeof(ITermoResponsabilidadeService), typeof(ModeloTermoService))
                .AddClasses()
                .AsImplementedInterfaces()
            );

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IPathHelper, PathHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            AlmocherifadoContext context, ApplicationDbContext applicationDbContext,
            UserManager<IdentityUser> userManager)
        {
            applicationDbContext.Database.Migrate();
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.ApplicationServices
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            var user = new IdentityUser("Admin3") { Email = "admin3@admin", EmailConfirmed = true };
            var hash = new PasswordHasher<IdentityUser>().HashPassword(null, "SenhaAdmin");
            user.PasswordHash = hash;
            var userStore = new UserStore<IdentityUser>(applicationDbContext);

            var result = userManager.CreateAsync(user, "Senhakrai").Result;


        }
    }
}
