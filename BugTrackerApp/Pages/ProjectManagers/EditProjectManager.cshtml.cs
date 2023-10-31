using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.ProjectManagers
{
	public class EditProjectManagerModel : PageModel
    {
        public readonly IPeopleDataAccess _peopleDataAccess;
        [BindProperty]
        public ProjectManager ProjectManager { get; set; }


        public EditProjectManagerModel(IPeopleDataAccess peopleDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
        }


        // Retrieves the selected project manager record in order
        // to populate the form fields with the selected project manager's attribute.
        public IActionResult OnGet(int id)
        {
            ProjectManager = _peopleDataAccess.ViewPerson(id, ProjectManager);
            return Page();
        }


        // Populates the form fields with the attributes of the selected
        // project manager and performs a full or partial update. When the
        // user enters new data into the form field, the employee record
        // is updated with the new values given.
        public IActionResult OnPost(int id, string phone, string zip, string address)
        {
            phone = ProjectManager.Phone;
            zip = ProjectManager.Zip;
            address = ProjectManager.Address;

            _peopleDataAccess.EditPerson(id, phone, zip, address, ProjectManager);
            return RedirectToPage("./ProjectManagerIndex");
        }
    }
}
