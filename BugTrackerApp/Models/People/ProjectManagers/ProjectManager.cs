using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Models
{
	public class ProjectManager : Person
	{
		[Required]
		public int ProjectManagerId { get; set; }

	}
}

