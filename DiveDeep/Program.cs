using DiveDeep.Data;
using DiveDeep.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DiveDeep
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DiveDeepContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            var app = builder.Build();

            // Opret database og seed data
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DiveDeepContext>();

                context.Database.EnsureCreated();

                //if (!context.Products.Any())
                //{
                //    var products = ProductRepository.GetAll();
                //    // Clear ProductId values to allow database to auto-generate them
                //    foreach (var product in products)
                //    {
                //        product.ProductId = 0;
                //    }
                //    context.Products.AddRange(products);
                //    context.SaveChanges();
                //}
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}