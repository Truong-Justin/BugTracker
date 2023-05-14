using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace BugTrackerApp.Models
{
	public class Bug : IEntity
	{
		// Id generation handled by database
		public int Id { get; set; }
		
		public DateOnly Date { get; set; }

		public string Description { get; set; }

		public string Priority { get; set; }
		
		public string Assignment { get; set; }
	}
}

