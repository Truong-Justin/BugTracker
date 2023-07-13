using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerApp.Pages
{
    public class AddBugModel : PageModel
    {
        public EntityDataAccess EntityDataAccess = new EntityDataAccess();
        public PeopleDataAccess PeopleDataAccess = new PeopleDataAccess();
        public Bug Bug { get; set; }
        public Project Project { get; set; }
        public required IList<Project> Projects { get; set; }
        public Employee Employee { get; set; }
        public required IList<Employee> Employees { get; set; }
        [BindProperty]
        public int SelectedProjectId { get; set; }
        [BindProperty]
        public string SelectedEmployee { get; set; }


        public ActionResult OnGet()
        {
            // Get a list of projects and use the project titles
            // for bug assignment 
            Projects = EntityDataAccess.GetAllEntities(Project);
            Employees = PeopleDataAccess.GetAllPeople(Employee);
            return Page();
        }
        
        public ActionResult OnPost(DateOnly date, string description, string priority)
        {
            try
            {
                if (SelectedEmployee == "0")
                {
                    SelectedEmployee = "Un-Assigned";
                }

                EntityDataAccess.AddEntity(SelectedProjectId, date, description, priority, SelectedEmployee, Bug);
                return RedirectToPage("./Index");
            }

            catch (Microsoft.Data.Sqlite.SqliteException)
            {
                return RedirectToPage("./Index");
            }
        }

    }
}