using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KittenJournal.Models
{
    public class Kitten
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kitten must have a name")]
        public string Name { get; set; }
        [DisplayName("Current Weight (grams)")]
        public int CurrentWeight { get; set; }
        public string Sex { get; set; }
        [DisplayName("Foster")]
        public int? FosterId { get; set; }

    }
}
