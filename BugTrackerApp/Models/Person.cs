using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Models
{
	// Abstract class created for code reusability 
    public abstract class Person
	{
		public int Id { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public DateOnly HireDate { get; set; }
	}
}

