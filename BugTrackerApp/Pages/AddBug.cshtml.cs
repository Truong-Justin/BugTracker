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
        BugDataAccess objBug = new BugDataAccess();

        public Bug bug { get; set; }
        public List<Bug> bugs { get;set; }
        public static int Id { get; set; }

        public ActionResult OnGet()
        {
            AddBugModel.Id = objBug.GetAllBugs().Count;
            return Page();
        }

        //Collects the user input and adds a new bug record
        //to the database
        public ActionResult OnPost(DateOnly Date, string Description, string Priority, string Assignment)
        {
            try
            {
                AddBugModel.Id++;
                objBug.addBug(Id, Date, Description, Priority, Assignment);
                return RedirectToPage("./Index");
            }

            //ensures a unique ID is generated and used
            catch (System.Data.SQLite.SQLiteException)
            {
                bugs = objBug.GetAllBugs();

                int newId = 0;
                foreach (Bug bug in bugs)
                {
                    if (bug.Id > newId)
                    {
                        newId = bug.Id;
                    }
                }

                newId++;

                objBug.addBug(newId, Date, Description, Priority, Assignment);
                return RedirectToPage("./Index");
            }
        }
        
    }
}
