using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KittenJournal.Models
{
    public class Feeding
    {
        public int Id { get; set; }
        [DisplayName("Starting Weight (grams)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a starting weight")]
        public int StartingWeight { get; set; }
        [DisplayName("Ending Weight (grams)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter an ending weight")]
        public int EndWeight { get; set; }
        [DisplayName("Date/Time")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a date and time")]
        public DateTime Timestamp { get; set; }
        public int KittenId { get; set; }

    }
}
