using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Models
{
    // Abstract class made to support inheritance
    // and code-reusability for future subclasses
    public abstract class Entity
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Priority { get; set; }
    }
}

