using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Projects
{
	public class EditProjectModel : PageModel
    {
        EntityDataAccess EntityDataAccess = new EntityDataAccess();
        [BindProperty]
        public Project Project { get; set; }


        public IActionResult OnGet(int id)
        {
            Project = EntityDataAccess.ViewEntity(id, Project);
            return Page();
        }

        public IActionResult OnPost(int id, DateOnly startDate, string projectTitle, string description, string priority)
        {
            startDate = Project.Date;
            projectTitle = Project.ProjectTitle;
            description = Project.Description;
            priority = Project.Priority;

            EntityDataAccess.EditEntity(id, startDate, projectTitle, description, priority, Project);

            return RedirectToPage("/Projects/ProjectIndex");
        }
    }
}
