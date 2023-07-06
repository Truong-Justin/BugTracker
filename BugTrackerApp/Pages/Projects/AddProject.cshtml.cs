using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BugTrackerApp.Pages
{
    public class AddProjectModel : PageModel
    {
        EntityDataAccess EntityDataAccess = new EntityDataAccess();
        PeopleDataAccess PeopleDataAccess = new PeopleDataAccess();
        public Project Project { get; set; }
        public required IList<Project> Projects { get; set; }
        public required ProjectManager ProjectManager { get; set; }
        public required IList<ProjectManager> ProjectManagers { get; set; }
        [BindProperty]
        public int SelectedProjectManagerId { get; set; }


        public ActionResult OnGet()
        {
            // Get a list of project managers and use their first name and
            // last name for project manager assignment
            ProjectManagers = PeopleDataAccess.GetAllPeople(ProjectManager);
            
            return Page();
        }


        public IEnumerable<SelectListItem> GetProjectManagers()
        {
            // Converts the list of Project Managers to
            // a collection of SelectListItem objects used 
            // to populate the asp-items for project manager
            return ProjectManagers.Select(pm => new SelectListItem { Value = pm.ProjectManagerId.ToString(), Text = pm.FirstName + " " + pm.LastName});
            
        }


        public ActionResult OnPost(DateOnly startDate, string projectTitle, string description, string priority)
        {
            try
            {
                EntityDataAccess.AddEntity(SelectedProjectManagerId, startDate, projectTitle, description, priority, Project);
                return RedirectToPage("./ProjectIndex");
            }

            catch (Microsoft.Data.Sqlite.SqliteException)
            {
                return RedirectToPage("./ProjectIndex");
            }
        }
    }
}
