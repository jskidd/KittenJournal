using KittenJournal.Models;
using KittenJournal.Models.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KittenJournal.DAL
{
    public static class SeedDB
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";
        public static void Migrate(IApplicationBuilder app)
        {
            AppDbContext ctx = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

            if (ctx.Database.GetPendingMigrations().Any())
            {
                ctx.Database.Migrate();
            }
        }

        public static async void SeedData(IApplicationBuilder app)
        {
            AppDbContext ctx = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

            if (!(await ctx.Kittens.AnyAsync())) {
                await ctx.Kittens.AddRangeAsync(
                    new Kitten
                    {
                        Name = "Rocky",
                        Sex = "Female",
                        CurrentWeight = 164
                    },
                    new Kitten
                    {
                        Name = "Canyon",
                        Sex  = "Male",
                        CurrentWeight = 273
                    },
                    new Kitten
                    {
                        Name = "Valley",
                        Sex = "Female",
                        CurrentWeight = 218
                    },
                    new Kitten { 
                        Name = "Dune",
                        Sex = "Male",
                        CurrentWeight = 226
                    },
                    new Kitten
                    {
                        Name = "Mesa",
                        Sex = "Female",
                        CurrentWeight = 249
                    },
                    new Kitten
                    {
                        Name = "Peninsula",
                        Sex = "Male",
                        CurrentWeight = 263
                    }
                    );
            }

            if (!(await ctx.Fosters.AnyAsync()))
            {
                await ctx.Fosters.AddRangeAsync(
                    new Foster
                    {
                        Name = "John & Stephanie Kidd",
                        Address = "753 Maple Dr",
                        City = "Cincinnati",
                        State = "OH",
                        ZipCode = "45215",
                        PhoneNumber = "513-370-1815",
                        Email = "john@jskidd.com"
                    }
                    );
            }

            if (!(await ctx.Feedings.AnyAsync()))
            {
                await ctx.Feedings.AddRangeAsync();
            }

            await ctx.SaveChangesAsync();
        }

        public static async void SeedIdentity(IApplicationBuilder app)
        {
            AppIdentityContext context = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<AppIdentityContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            UserManager<KittenJournalUser> userManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<UserManager<KittenJournalUser>>();
            KittenJournalUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new KittenJournalUser("Admin");
                user.Email = "admin@example.com";
                user.PhoneNumber = "555-1234";
                await userManager.CreateAsync(user, adminPassword);
            }

            RoleManager<IdentityRole> roleManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityRole adminRole = await roleManager.FindByNameAsync("Administrator");
            if (adminRole == null)
            { 
                adminRole = new IdentityRole("Administrator");
                await roleManager.CreateAsync(adminRole);
            }

            await userManager.AddToRoleAsync(user, "Administrator");

            IdentityRole fosterRole = await roleManager.FindByNameAsync("Foster");
            if (fosterRole == null)
            {
                fosterRole = new IdentityRole("Foster");
                await roleManager.CreateAsync(fosterRole);
            }
            
        }
    }
}
