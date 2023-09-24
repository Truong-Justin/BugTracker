using BugTrackerApp.Models.People;
using BugTrackerApp.Models;
using BugTrackerApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Employees
{
	public class ViewEmployeeModel : PageModel
    {
        public readonly IPeopleDataAccess _peopleDataAccess;
        public readonly IEntityDataAccess _entityDataAccess;
        public required Employee Employee { get; set; }
        public required Project Project { get; set; }


        public ViewEmployeeModel(IPeopleDataAccess peopleDataAccess, IEntityDataAccess entityDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
            _entityDataAccess = entityDataAccess;
        }

        // A specific employee record is retrieved
        // from the Employees table, and that employee
        // record's project id is used to retrieve the
        // project the employee is assigned to.
        public void OnGet(int id)
        {
            Employee = _peopleDataAccess.ViewPerson(id, Employee);
            Project = _entityDataAccess.ViewEntity(Employee.ProjectId, Project);
        }

        public ActionResult OnPost(int id)
        {
            _peopleDataAccess.DeletePerson(id, Employee);
            return RedirectToPage("./EmployeeIndex");
        }
    }
}
