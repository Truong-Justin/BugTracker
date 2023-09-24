using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using BugTrackerApp.Models.Entities;

namespace BugTrackerApp.Pages
{
    public class AddBugModel : PageModel
    {
        public readonly IEntityDataAccess _entityDataAccess;
        public readonly IPeopleDataAccess _peopleDataAccess;
        public Bug Bug { get; set; }
        [BindProperty]
        public Project Project { get; set; }
        public required IList<Project> Projects { get; set; }
        public Employee Employee { get; set; }
        public required IList<Employee> Employees { get; set; }
        [BindProperty]
        public string SelectedEmployee { get; set; }

        public AddBugModel(IEntityDataAccess entityDataAccess, IPeopleDataAccess peopleDataAccess)
        {
            _entityDataAccess = entityDataAccess;
            _peopleDataAccess = peopleDataAccess;
        }

        public ActionResult OnGet()
        {
            // Get a list of projects and employees,
            // and use the project titles and employee names
            // for bug assignment.
            Projects = _entityDataAccess.GetAllEntities(Project);
            Employees = _peopleDataAccess.GetAllPeople(Employee);
            return Page();
        }

        public ActionResult OnPost(DateOnly date, string description, string priority)
        {
            // Add a new bug record to the Bugs table.
            try
            {
                _entityDataAccess.AddEntity(Project.ProjectId, date, description, priority, SelectedEmployee, Bug);
                return RedirectToPage("./Index");
            }

            // If an exception occurs, redirect to the
            // bug index page.
            catch (Exception exception)
            {
                Console.WriteLine("Error, exception: " + exception);
                return RedirectToPage("./Index");
            }
        }

    }
}