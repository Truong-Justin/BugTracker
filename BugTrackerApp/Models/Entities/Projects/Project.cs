using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerApp.Models
{
	public class Project : Entity
	{
		// Foreign Key
		public int ProjectManagerId { get; set; }

		public Project ProjectManager { get; set; }

		// Collection of bugs that a project has
		IList<Bug> Bugs { get; set; }

		// Collection of employees that a project has
		IList<Employee> Employees { get; set; }
	}
}

