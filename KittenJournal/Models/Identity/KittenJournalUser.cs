using Microsoft.AspNetCore.Identity;

namespace KittenJournal.Models.Identity
{
    public class KittenJournalUser : IdentityUser
    {
        public int FosterId { get; set; }

        public KittenJournalUser() : base()
        {

        }

        public KittenJournalUser(string user) : base(user)
        {

        }
    }
}
