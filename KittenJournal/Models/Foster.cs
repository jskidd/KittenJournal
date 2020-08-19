using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KittenJournal.Models
{
    public class Foster
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [DisplayName("Foster Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

    }
}
