using KittenJournal.Models;
using Microsoft.AspNetCore.Builder;
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
    }
}
