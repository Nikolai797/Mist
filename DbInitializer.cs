using GameStore.Data.Entities;
using GameStore.Data.Identity;
using GameStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Services;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
   
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = new[] { "Admin", "Customer" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // ---- 2. АДМИНИСТРАТОР ----
        var adminEmail = "admin@game.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "User"
            };
            var result = await userManager.CreateAsync(user, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        // ---- 3. ТЕСТОВ ПОТРЕБИТЕЛ (CUSTOMER) ----
        var customerEmail = "customer@game.com";
        var customerUser = await userManager.FindByEmailAsync(customerEmail);
        if (customerUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = customerEmail,
                Email = customerEmail,
                FirstName = "John",
                LastName = "Doe"
            };
            var result = await userManager.CreateAsync(user, "Customer123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Customer");
            }
        }

        // ---- 4. SEED – КАТЕГОРИИ И ИГРИ ----
        var categoryService = serviceProvider.GetRequiredService<ICategoryService>();
        var gameService = serviceProvider.GetRequiredService<IGameService>();

        // --- Категории ---
        if (!(await categoryService.GetAllCategoriesAsync()).Any())
        {
            var categories = new List<Category>
            {
                new Category { Name = "Action" },
                new Category { Name = "RPG" },
                new Category { Name = "Strategy" },
                new Category { Name = "Sports" }
            };

            foreach (var category in categories)
            {
                await categoryService.CreateCategoryAsync(category);
            }
        }

        // --- Игри ---
        if (!(await gameService.GetAllGamesAsync()).Any())
        {
            var categories = await categoryService.GetAllCategoriesAsync();

            var games = new List<Game>
            {
                new Game
                {
                    Title = "Cyberpunk 2077",
                    Description = "Open-world RPG in Night City",
                    Price = 59.99m,
                    CategoryId = categories.First(c => c.Name == "RPG").Id
                },
                new Game
                {
                    Title = "EA Sports FC 25",
                    Description = "Football simulation",
                    Price = 69.99m,
                    CategoryId = categories.First(c => c.Name == "Sports").Id
                },
                new Game
                {
                    Title = "Starfield",
                    Description = "Space exploration RPG",
                    Price = 69.99m,
                    CategoryId = categories.First(c => c.Name == "RPG").Id
                },
                new Game
                {
                    Title = "Call of Duty: Modern Warfare II",
                    Description = "First-person shooter",
                    Price = 59.99m,
                    CategoryId = categories.First(c => c.Name == "Action").Id
                },
                new Game
                {
                    Title = "Civilization VII",
                    Description = "Turn-based strategy",
                    Price = 59.99m,
                    CategoryId = categories.First(c => c.Name == "Strategy").Id
                }
            };

            foreach (var game in games)
            {
                await gameService.CreateGameAsync(game);
            }
        }
    }
}
