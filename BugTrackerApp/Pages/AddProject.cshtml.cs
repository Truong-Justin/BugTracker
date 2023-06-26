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


// !--View-Model currently going through troubleshooting process;
// PeopleDataAccess.GetAllEntities() is returning a null object instead
// of a populated list of project managers--!

namespace BugTrackerApp.Pages
{
    public class AddProjectModel : PageModel
    {
        EntityDataAccess EntityDataAccess = new EntityDataAccess();
        PeopleDataAccess PeopleDataAccess = new PeopleDataAccess();
        public Project Project { get; set; }
        public IList<Project> Projects { get; set; }
        public ProjectManager ProjectManager { get; set; }
        public IList<ProjectManager> ProjectManagers { get; set; }
        [BindProperty]
        public int SelectedProjectManagerId { get; set; }


        public ActionResult OnGet()
        {
            // Get a list of project managers and use their first name and
            // last name for project manager assignment
            ProjectManagers = PeopleDataAccess.GetAllEntities(ProjectManager);
            
            return Page();
        }


        public IEnumerable<SelectListItem> GetProjectManagers()
        {
            // Converts the list of Project Managers to
            // a collection of SelectListItem objects
            // used to populate the asp-items for project manager

            try
            {
                return ProjectManagers.Select(pm => new SelectListItem { Value = pm.ProjectManagerId.ToString(), Text = pm.FirstName });
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return ProjectManagers.Select(pm => new SelectListItem { Value = pm.ProjectManagerId.ToString(), Text = pm.FirstName });
            }
        }

        // Test Method...Delete later
        public IEnumerable<SelectListItem> GetProjectManagerss()
        {
            var ProjectManagers = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Kenny"},
                new SelectListItem { Value = "2", Text = "Uncle Tommy"}
            };

            Console.WriteLine(ProjectManagers);

            return ProjectManagers;
        }


        public ActionResult OnPost(string description, DateOnly date, string priority, string assignment)
        {
            try
            {
                EntityDataAccess.AddEntity(date, description, priority, assignment, SelectedProjectManagerId, Project);
                return RedirectToPage("./ProjectIndex");
            }

            catch (Microsoft.Data.Sqlite.SqliteException)
            {
                return RedirectToPage("./ProjectIndex");
            }
        }
    }
}
