using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTrackerApp.Models;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerApp.Pages
{
    public class AddBugModel : PageModel
    {
        EntityDataAccess ObjDataAccess = new EntityDataAccess();
        public Bug Bug { get; set; }
        public IList<Bug> Bugs { get; set; }
        public static int Id { get; set; }

        public Project Project { get; set; }
        public IList<Project> Projects { get; set; }
        [BindProperty]
        public int SelectedProjectId { get; set; }


        public ActionResult OnGet()
        {
            // Used for ID generation
            AddBugModel.Id = ObjDataAccess.GetAllEntities(Bug).Count;

            Projects = ObjDataAccess.GetAllEntities(Project);

            return Page();
        }

        public IEnumerable<SelectListItem> GetProjectDescriptions()
        {
            // Converts the list of Projects to
            // a collection of SelectListItem objects
            // used to populate the asp-items for project assignment
            return Projects.Select(project => new SelectListItem { Value = project.ProjectId.ToString(), Text = project.ProjectTitle});
        }

        // Collects the user input and adds a new bug record
        // to the database.
        public ActionResult OnPost(DateOnly date, string description, string priority, string assignment)
        {
            try
            {
                AddBugModel.Id++;
                ObjDataAccess.AddEntity(Id, SelectedProjectId, date, description, priority, assignment, Bug);
                return RedirectToPage("./Index");
            }

            // Ensures a unique ID is generated and used.
            catch (Microsoft.Data.Sqlite.SqliteException)
            {
                Bugs = ObjDataAccess.GetAllEntities(Bug);

                int newId = AddBugModel.Id;
                foreach (Bug bug in Bugs)
                {
                    if (bug.BugId > newId)
                    {
                        newId = bug.BugId;
                    }
                }

                newId++;

                ObjDataAccess.AddEntity(newId, SelectedProjectId, date, description, priority, assignment, Bug);
                return RedirectToPage("./Index");
            }
        }

    }
}