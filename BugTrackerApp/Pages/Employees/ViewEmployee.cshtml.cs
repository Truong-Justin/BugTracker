using BugTrackerApp.Models.People;
using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Employees
{
	public class ViewEmployeeModel : PageModel
    {
        PeopleDataAccess PeopleDataAccess = new PeopleDataAccess();
        public required Employee Employee { get; set; }

        public void OnGet(int id)
        {
            Employee = PeopleDataAccess.ViewPerson(id, Employee);
        }

        public ActionResult OnPost(int id)
        {
            PeopleDataAccess.DeletePerson(id, Employee);
            return RedirectToPage("Employees/EmployeeIndex");
        }
    }
}
