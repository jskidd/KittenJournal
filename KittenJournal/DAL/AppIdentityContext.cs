using KittenJournal.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KittenJournal.DAL
{
    public class AppIdentityContext : IdentityDbContext<KittenJournalUser>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> opts) : base(opts)
        {

        }
    }
}
