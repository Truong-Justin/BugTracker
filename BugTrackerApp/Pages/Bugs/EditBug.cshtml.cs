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
        // the bug record is updated with new values.
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
