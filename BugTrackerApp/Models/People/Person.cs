using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Models
{

    // Abstract class made to support inheritance
    // and code-reusability for future subclasses
    public abstract class Person
	{

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}"), Required]
        public DateOnly HireDate { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Address { get; set; }
    }
}

