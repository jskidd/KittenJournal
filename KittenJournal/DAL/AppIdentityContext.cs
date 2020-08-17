using KittenJournal.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KittenJournal.DAL
{
    public class AppIdentityContext : IdentityDbContext<KittenJournalUser>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> opts) : base(opts)
        {

        }
    }
}
