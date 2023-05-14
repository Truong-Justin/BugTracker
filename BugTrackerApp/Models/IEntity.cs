using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Models
{
    // Interface created to allow for extensibility
    // and loose coupling as more entity classes will
    // be added
    public interface IEntity
    {

        int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}"), Required]
        DateOnly Date { get; set; }

        [Required]
        string Description { get; set; }

        [Required]
        string Priority { get; set; }

        [Required]
        string Assignment { get; set; }
    }
}

