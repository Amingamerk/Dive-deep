using DiveDeep.Data;
using DiveDeep.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DiveDeep.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>()
                .CreateLogger("SeedData");

            try
            {
                var context = serviceProvider.GetRequiredService<DiveDeepContext>();

                // Kør migrationer
                context.Database.Migrate();

                // Seed produkter
                await SeedProductsAsync(context);

                // Seed roller og admin-bruger
                await SeedRolesAndAdminAsync(serviceProvider, configuration);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Der opstod en fejl under migrering eller seeding af data.");
            }
        }

        private static async Task SeedProductsAsync(DiveDeepContext context)
        {
            if (!context.Products.Any())
            {
                var products = InMemoryProductRepository.GetAll();

                // Clear ProductId values to allow database to auto-generate them
                foreach (var product in products)
                {
                    product.ProductId = 0;
                }

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // 1. Opret rollen "Admin", hvis den ikke allerede findes
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // 2. Opret en admin-bruger, hvis den ikke allerede findes
            var adminEmail = configuration["AdminSeed:Email"];
            var adminPassword = configuration["AdminSeed:Password"];

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            else if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}