using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerApp.Models
{
	public class Project : IEntity
	{
		public int Id { get; set; }

		public DateOnly Date { get;set; }

		public string Description { get; set; }

		public string Assignment { get; set; }

		public string Priority { get; set; }

		// Foreign Key
		public int ProjectManagerId { get; set; }
	}
}

