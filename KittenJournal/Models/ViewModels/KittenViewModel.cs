using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KittenJournal.Models.ViewModels
{
    public class KittenViewModel
    {
        public Kitten Kitten { get; set; }
        public Foster Foster { get; set; }
        public IEnumerable<Feeding> Feedings { get; set; }
    }
}
