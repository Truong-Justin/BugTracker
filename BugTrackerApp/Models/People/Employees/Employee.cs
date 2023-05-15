using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Models
{
	public class Employee : Person
	{
		// foreign key
		public int ProjectId { get; set; }
	}
}

