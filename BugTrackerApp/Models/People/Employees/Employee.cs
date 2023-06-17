using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Models
{
	public class Employee : Person
	{

		public int EmployeeId { get; set; }

		// foreign key
		public int ProjectId { get; set; }

	}
}

