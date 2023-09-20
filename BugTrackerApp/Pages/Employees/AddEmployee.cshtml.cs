using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Employees
{
    public class AddEmployeeModel : PageModel
    {
        public readonly PeopleDataAccess _peopleDataAccess;
        public readonly EntityDataAccess _entityDataAccess;
        [BindProperty]
        public required Employee Employee { get; set; }
        public Project Project { get; set; }
        public IList<Project> Projects { get; set; }

        public AddEmployeeModel(PeopleDataAccess peopleDataAccess, EntityDataAccess entityDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
            _entityDataAccess = entityDataAccess;
        }

        public ActionResult OnGet()
        {
            Projects = _entityDataAccess.GetAllEntities(Project);
            return Page();
        }

        public ActionResult OnPost(string firstName, string lastName, DateOnly hireDate, string phone, string zip, string address)
        {
            // Add a new employee record to the
            // Employees table
            try
            {
                _peopleDataAccess.AddPerson(firstName, lastName, hireDate, phone, zip, address, Employee.ProjectId, Employee);
                return RedirectToPage("./EmployeeIndex");
            }

            // If an exception occurs while trying
            // to insert a new employee record,
            // redirect to the employee index page.
            catch(Exception exception)
            {
                Console.WriteLine("Error, exception: " + exception);
                return RedirectToPage("./EmployeeIndex");
            }
        }
    }
}
