using System.Collections.Generic;
using System.Linq;

namespace KittenJournal.Models.ViewModels
{
    public class FosterViewModel
    {
        public Foster foster { get; set; }
        public IEnumerable<Kitten> kittens { get; set; }
        public int KittensCount { get => kittens.Count(); }
    }
}
