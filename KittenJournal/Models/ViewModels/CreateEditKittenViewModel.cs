using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KittenJournal.Models.ViewModels
{
    public class CreateEditKittenViewModel
    {
        private List<string> _sex = new List<string>()
        {
            "Male",
            "Female",
            "Unknown"
        };

        public CreateEditKittenViewModel()
        {
            kitten = new Kitten();
        }

        public CreateEditKittenViewModel(Kitten ktn)
        {
            kitten = ktn;
        }
        public Kitten kitten { get; set; }
        public int Id { get => kitten.Id; }
        public string Name { get => kitten.Name; set => kitten.Name = value; }
        public int CurrentWeight { get => kitten.CurrentWeight; set => kitten.CurrentWeight = value; }
        public string Sex { get => kitten.Sex; set => kitten.Sex = value; }
        public int? FosterId { get => kitten.FosterId; set => kitten.FosterId = value; }
        public IEnumerable<Foster> FostersList { get; set; }
        public IEnumerable<string> SexList { get => _sex.ToList(); }
    }
}
