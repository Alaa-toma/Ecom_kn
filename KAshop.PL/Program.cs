
using KAshop.BLL.Service;
using KAshop.DAL.Data;
using KAshop.DAL.Models;
using KAshop.DAL.Repository;
using KAshop.PL.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace KAshop.PL
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.






            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });




            builder.Services.AddLocalization(options => options.ResourcesPath = "");

            const string defaultCulture = "en";

            var supportedCultures = new[]
            {
                 new CultureInfo(defaultCulture),
                 new CultureInfo("ar")
            };

            builder.Services.Configure<RequestLocalizationOptions>(options => {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders.Clear();

                options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
            });
            
            // objects............
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();



            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // .................

            var app = builder.Build();

            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
