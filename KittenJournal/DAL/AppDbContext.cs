using KittenJournal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KittenJournal.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> ops) : base(ops)
        {

        }

        public DbSet<Foster> Fosters { get; set; }
        public DbSet<Kitten> Kittens { get; set; }
        public DbSet<Feeding> Feedings { get; set; }

    }
}
