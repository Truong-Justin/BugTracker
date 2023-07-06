using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BugTrackerApp.Pages
{
	public class ProjectManagerIndexModel : PageModel
    {
        PeopleDataAccess ObjDataAccess = new PeopleDataAccess();
        public required ProjectManager ProjectManager { get; set; }
        public required IList<ProjectManager> ProjectManagers { get; set; }

        public ActionResult OnGet()
        {
            ProjectManagers = ObjDataAccess.GetAllPeople(ProjectManager);
            return Page();
        }
    }
}
