using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerApp.Models
{
	public class Project : Entity
	{
		// Foreign Key
		public int ProjectManagerId { get; set; }
	}
}

