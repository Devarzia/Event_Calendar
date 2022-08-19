using System.ComponentModel.DataAnnotations;

namespace EventCalendar.Application
{
    public class EventCategoryDTO
    {
        public int EventCategoryID { get; set; }
        [Required(ErrorMessage = "{0} is required"), MaxLength(25, ErrorMessage = "{0} is required"), Display(Name = "Category Name")]
        public string EventCategoryName { get; set; }
    }
}
