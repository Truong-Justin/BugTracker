using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.ProjectManagers
{
	public class AddProjectManagerModel : PageModel
    {
        PeopleDataAccess PeopleDataAccess = new PeopleDataAccess();
        public required ProjectManager ProjectManager { get; set; }

        public ActionResult OnPost(string firstName, string lastName, DateOnly hireDate, string phone, string zip, string address)
        {
            try
            {
                PeopleDataAccess.AddPerson(firstName, lastName, hireDate, phone, zip, address, ProjectManager);
                return RedirectToPage("./ProjectManagerIndex");
            }

            catch (Microsoft.Data.Sqlite.SqliteException)
            {
                return RedirectToPage("./ProjectManagerIndex");
            }
        }
    }
}
