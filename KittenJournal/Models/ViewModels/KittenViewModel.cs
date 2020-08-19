using System.Collections.Generic;

namespace KittenJournal.Models.ViewModels
{
    public class KittenViewModel
    {
        public Kitten Kitten { get; set; }
        public Foster Foster { get; set; }
        public IEnumerable<Feeding> Feedings { get; set; }
    }
}
