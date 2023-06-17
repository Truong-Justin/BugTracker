using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Models
{
	public class Bug : Entity
	{
        [Required]
        public int BugId { get; set; }

        [Required]
        public string Assignment { get; set; }

        // Foreign key
        public int ProjectId { get; set; }
    }
}

