using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
