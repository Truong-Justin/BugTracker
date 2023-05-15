using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTrackerApp.Models;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerApp.Pages
{
    public class AddBugModel : PageModel
    {
        BugDataAccess ObjBug = new BugDataAccess();
        public Bug Bug { get; set; }
        public IList<Bug> Bugs { get; set; }
        public static int Id { get; set; }

        public ActionResult OnGet()
        {
            // Returns the length of the bugs list returned from the
            // bugs database that is used to generate an Id to be used
            // for a new bug record
            AddBugModel.Id = ObjBug.GetAllEntities(Bug).Count;

            return Page();
        }

        // Collects the user input and adds a new bug record
        // to the database.
        public ActionResult OnPost(DateOnly date, string description, string priority, string assignment)
        {
            try
            {
                AddBugModel.Id++;
                ObjBug.AddEntity(Id, date, description, priority, assignment, Bug);
                return RedirectToPage("./Index");
            }

            // Ensures a unique ID is generated and used.
            catch (System.Data.SQLite.SQLiteException)
            {
                Bugs = (IList<Bug>)ObjBug.GetAllEntities(Bug);

                int newId = AddBugModel.Id;
                foreach (Bug bug in Bugs)
                {
                    if (bug.Id > newId)
                    {
                        newId = bug.Id;
                    }
                }

                newId++;

                ObjBug.AddEntity(newId, date, description, priority, assignment, Bug);
                return RedirectToPage("./Index");
            }
        }

    }
}