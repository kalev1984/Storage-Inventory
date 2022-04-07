using App.DAL.EF;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Helpers;

public static class AppDataHelper
{
    public static async void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        await using var context = serviceScope
            .ServiceProvider.GetService<AppDbContext>();

        if (context == null)
        {
            throw new ApplicationException("Problem in services. No db context.");
        }

        if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            context.Database.EnsureDeleted();
        }

        if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            context.Database.Migrate();
        }

        if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

            if (userManager == null || roleManager == null)
            {
                throw new NullReferenceException("userManager or roleManager cannot be null!");
            }
            
            var role = roleManager.FindByNameAsync("admin").Result;
            if (role == null)
            {
                var identityResult = roleManager.CreateAsync(new AppRole
                {
                    Name = "admin",
                    NormalizedName = "administrator role"
                }).Result;
                if (!identityResult.Succeeded)
                {
                    throw new ApplicationException("Role creation failed");
                }
            }

            var user = userManager.FindByEmailAsync("admin@kkilum.com").Result ?? new AppUser
            {
                Email = "admin@kkilum.com",
                UserName = "admin@kkilum.com",
                EmailConfirmed = true
            };

            var identityUserResult = userManager.CreateAsync(user, "Kala.maja1").Result;
            if (!identityUserResult.Succeeded)
            {
                throw new ApplicationException("Cannot create user!");
            }

            await userManager.AddToRoleAsync(user, "admin");
        }
    }
}