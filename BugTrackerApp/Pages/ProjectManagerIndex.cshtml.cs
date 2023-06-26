using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// !--View-Model currently going through troubleshooting process;
// PeopleDataAccess.GetAllEntities() is returning a null object instead
// of a populated list of project managers--!

namespace BugTrackerApp.Pages
{
	public class ProjectManagerIndexModel : PageModel
    {
        PeopleDataAccess ObjDataAccess = new PeopleDataAccess();
        ProjectManager ProjectManager { get; set; }
        public IList<ProjectManager> ProjectManagers { get; set; }

        public ActionResult OnGet()
        {
            ProjectManagers = ObjDataAccess.GetAllEntities(ProjectManager);
            foreach (ProjectManager PM in ProjectManagers)
            {
                Console.WriteLine(PM.FirstName);
            }
            return Page();
        }
    }
}
