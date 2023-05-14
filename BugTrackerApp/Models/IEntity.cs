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

        DateOnly Date { get; set; }

        string Description { get; set; }

        string Priority { get; set; }

        string Assignment { get; set; }
    }
}

