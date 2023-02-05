using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace BugTrackerApp.Models
{
	//Class represents the data/attributes
	//found in the Bugs database
	public class Bug
	{
		public int Id { get; set; }

		[DisplayFormat(DataFormatString ="{0:MM-dd-yyyy}"), Required]
		public DateOnly Date { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public string Priority { get; set; }

		[Required]
		public string Assignment { get; set; }


	}
}

