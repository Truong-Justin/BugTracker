using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using BugTrackerApp.Models.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Employees
{
	public class EmployeeIndexModel : PageModel
    {
        public readonly IPeopleDataAccess _peopleDataAccess;
        public required Employee Employee { get; set; }
        public required IList<Employee> Employees { get; set; }

        public EmployeeIndexModel(IPeopleDataAccess peopleDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
        }

        public void OnGet()
        {
            Employees = _peopleDataAccess.GetAllPeople(Employee);
        }
    }
}
