using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTrackerApp.Models;
using BugTrackerApp.Models.People;

namespace BugTrackerApp.Pages
{
    public class AddBugModel : PageModel
    {
        public readonly EntityDataAccess _entityDataAccess;
        public readonly PeopleDataAccess _peopleDataAccess;
        public Bug Bug { get; set; }
        public Project Project { get; set; }
        public required IList<Project> Projects { get; set; }
        public Employee Employee { get; set; }
        public required IList<Employee> Employees { get; set; }
        [BindProperty]
        public int SelectedProjectId { get; set; }
        [BindProperty]
        public string SelectedEmployee { get; set; }

        public AddBugModel(EntityDataAccess entityDataAccess, PeopleDataAccess peopleDataAccess)
        {
            _entityDataAccess = entityDataAccess;
            _peopleDataAccess = peopleDataAccess;
        }


        public ActionResult OnGet()
        {
            // Get a list of projects and use the project titles
            // for bug assignment 
            Projects = _entityDataAccess.GetAllEntities(Project);
            Employees = _peopleDataAccess.GetAllPeople(Employee);
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

                _entityDataAccess.AddEntity(SelectedProjectId, date, description, priority, SelectedEmployee, Bug);
                return RedirectToPage("./Index");
            }

            catch (Exception exception)
            {
                Console.WriteLine("Error, exception: " + exception);
                return RedirectToPage("./Index");
            }
        }

    }
}