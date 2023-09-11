using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages
{
	public class EditBugModel : PageModel
    {
        public readonly EntityDataAccess _entityDataAccess;
        public readonly PeopleDataAccess _peopleDataAccess;
        public Employee Employee { get; set; }
        public IList<Employee> Employees { get; set; }
        [BindProperty]
        public Bug Bug { get; set; }
        [BindProperty]
        public string SelectedEmployee { get; set; }


        public EditBugModel(EntityDataAccess entityDataAccess, PeopleDataAccess peopleDataAccess)
        {
            _entityDataAccess = entityDataAccess;
            _peopleDataAccess = peopleDataAccess;
        }

        public IActionResult OnGet(int Id)
        {
            Bug = _entityDataAccess.ViewEntity(Id, Bug);
            Employees = _peopleDataAccess.GetAllPeople(Employee);
            return Page();
        }

        // Populate the form fields with the attributes of the selected
        // bug the user wants to update.
        // When the user enters new data into the form,
        // the bug record is updated.
        public IActionResult OnPost(int id, string description, string priority, string assignment)
        {
            
            Bug = _entityDataAccess.ViewEntity(id, Bug);
            description = Bug.Description;
            priority = Bug.Priority;
            assignment = Bug.Assignment;

            // If a user chooses a new employee from the
            // drop-down menu, the new record's assignment
            // is set to the selected employee
            if (SelectedEmployee != "0")
            {
                assignment = SelectedEmployee;
            }

            _entityDataAccess.EditEntity(id, description, priority, assignment, Bug);

            return RedirectToPage("Index");
        }
    }
}
