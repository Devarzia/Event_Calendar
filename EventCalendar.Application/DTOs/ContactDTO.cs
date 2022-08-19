using System.ComponentModel.DataAnnotations;

namespace EventCalendar.Application
{
    public class ContactDTO
    {
        public int ContactID { get; set; }
        [Required(ErrorMessage = "{0} is required"), StringLength(35), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} is required"), StringLength(35), Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "{0} is required"), EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} is required"), MaxLength(200)]
        public string Message { get; set; }
    }
}
