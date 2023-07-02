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
        public Project Project { get; set; }
        public required ProjectManager ProjectManager { get; set; }

        public void OnGet(int id)
        {
            Project = EntityDataAccess.ViewEntity(id, Project);
            //ProjectManager = PeopleDataAccess.ViewPerson(Project.ProjectId, ProjectManager);
        }
    }
}
