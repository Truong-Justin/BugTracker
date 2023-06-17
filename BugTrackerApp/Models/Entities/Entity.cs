using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Models
{
    // Abstract class made to support inheritance
    // and code-reusability for future subclasses
    public abstract class Entity
    {

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}"), Required]
        public DateOnly Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Priority { get; set; }


        // Constructor sets default values
        // to avoid possible null references
        public Entity()
        {
            Description = string.Empty;
            Priority = string.Empty;
        }
    }
}

