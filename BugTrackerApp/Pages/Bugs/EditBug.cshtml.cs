using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using BugTrackerApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages
{
	public class EditBugModel : PageModel
    {
        public readonly IEntityDataAccess _entityDataAccess;
        public readonly IPeopleDataAccess _peopleDataAccess;
        public Employee Employee { get; set; }
        public IList<Employee> Employees { get; set; }
        [BindProperty]
        public Bug Bug { get; set; }


        public EditBugModel(IEntityDataAccess entityDataAccess, IPeopleDataAccess peopleDataAccess)
        {
            _entityDataAccess = entityDataAccess;
            _peopleDataAccess = peopleDataAccess;
        }


        // Method retrieves the selected bug record in order to populate
        // the form fields with the selected bug's attributes.

        // Then a list of all employee records is retrieved from the database
        // and used to populate a <select> HTML element, so the user can choose
        // to assign a different employee to work on the bug. 
        public IActionResult OnGet(int Id)
        {
            Bug = _entityDataAccess.ViewEntity(Id, Bug);
            Employees = _peopleDataAccess.GetAllPeople(Employee);
            return Page();
        }

        // Populates the form fields with the attributes of the selected
        // bug and performs a full or partial update. When the
        // user enters new data into the form field, the bug record
        // is updated with the new values given.
        public IActionResult OnPost(int id, string description, string priority, string assignment)
        {
            description = Bug.Description;
            priority = Bug.Priority;
            assignment = Bug.Assignment;

            _entityDataAccess.EditEntity(id, description, priority, assignment, Bug);
            return RedirectToPage("Index");
        }
    }
}
