using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Projects
{
	public class ViewProjectModel : PageModel
    {
        EntityDataAccess EntityDataAccess = new EntityDataAccess();
        PeopleDataAccess PeopleDataAccess = new PeopleDataAccess();
        public required Project Project { get; set; }
        public ProjectManager ProjectManager { get; set; }


        public void OnGet(int id)
        {
            Project = EntityDataAccess.ViewEntity(id, Project);
            ProjectManager = PeopleDataAccess.ViewPerson(Project.ProjectManagerId, ProjectManager);
        }

        public ActionResult OnPost(int id)
        {
            EntityDataAccess.DeleteEntity(id, Project);
            return RedirectToPage("/Projects/ProjectIndex");
        }
    }
}
