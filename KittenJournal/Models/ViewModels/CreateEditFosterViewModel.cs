using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KittenJournal.Models.ViewModels
{
    public class CreateEditFosterViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Please enter an email address")]
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please confirm the password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
