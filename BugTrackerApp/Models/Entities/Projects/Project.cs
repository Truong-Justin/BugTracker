using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Models
{
	public class Project : Entity
	{
		[Required]
		public int ProjectId { get; set; }

		[Required]
		public string ProjectTitle { get; set; }

		// Foreign Key
		public int ProjectManagerId { get; set; }
	}
}

