using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Models
{
	public class Bug : Entity
	{
        // Foreign key
        public int ProjectId { get; set; }
    }
}

