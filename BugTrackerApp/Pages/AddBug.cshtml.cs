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

        public Bug? bug { get; set; }
        public static int nextId = 2;
        

        //Collects the user input and adds a new bug record
        //to the database
        public ActionResult OnPost(string Date, string Description, string Priority, string Assignment)
        {
            AddBugModel.nextId++;
            objBug.addBug(nextId, Date, Description, Priority, Assignment);
            return RedirectToPage("./Index");
        }
        
    }
}
