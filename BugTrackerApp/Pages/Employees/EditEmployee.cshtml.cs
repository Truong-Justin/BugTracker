using BugTrackerApp.Models.Entities;
using BugTrackerApp.Models.People;
using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;


namespace BugTrackerApp.Pages.Employees
{
	public class EditEmployeeModel : PageModel
    {
        public readonly IEntityDataAccess _entityDataAccess;
        public readonly IPeopleDataAccess _peopleDataAccess;
        public Project Project { get; set; }
        public IList<Project> Projects { get; set; }
        [BindProperty]
        public Employee Employee { get; set; }


        public EditEmployeeModel(IEntityDataAccess entityDataAccess, IPeopleDataAccess peopleDataAccess)
        {
            _entityDataAccess = entityDataAccess;
            _peopleDataAccess = peopleDataAccess;
        }


        // Method retrieves the selected employee record in order to
        // populate the form fields with the selected employee's attributes.

        // Then a list of all project records is retrieved from the database
        // and used to populate the select HTML element, so the user can choose
        // to re-assign an employee to a different project.
        public IActionResult OnGet(int id)
        {
            Employee = _peopleDataAccess.ViewPerson(id, Employee);
            Projects = _entityDataAccess.GetAllEntities(Project);
            return Page();
        }


        // Populates the form fields with the attributes of the selected
        // employee and performs a full or partial update. When the
        // user enters new data into the form field, the employee record
        // is updated with the new values given.
        public IActionResult OnPost(int id, string address, string zip, string phone, int projectId)
        {
            address = Employee.Address;
            zip = Employee.Zip;
            phone = Employee.Phone;
            projectId = Employee.ProjectId;

            _peopleDataAccess.EditPerson(id, phone, zip, address, projectId, Employee);
            return RedirectToPage("./EmployeeIndex");
        }
    }
}
