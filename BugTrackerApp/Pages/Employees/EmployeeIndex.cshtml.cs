using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Employees
{
	public class EmployeeIndexModel : PageModel
    {
        PeopleDataAccess PeopleDataAccess = new PeopleDataAccess();
        public required Employee Employee { get; set; }
        public required IList<Employee> Employees { get; set; }

        public ActionResult OnGet()
        {
            Employees = PeopleDataAccess.GetAllPeople(Employee);
            return Page();
        }
    }
}
