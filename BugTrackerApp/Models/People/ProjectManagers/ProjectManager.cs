using System;

namespace BugTrackerApp.Models
{
	public class ProjectManager : Person
	{

		// Collection of projects that a project
		// manager is in charge of
		public IList<Project> Projects { get; set; }
	}
}

