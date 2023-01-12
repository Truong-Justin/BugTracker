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
        //BugDataAccess objBug = new BugDataAccess();

        public Bug bug { get; set; }
        BugDataAccess bugObj = new BugDataAccess();


        public ActionResult OnPost(int Id, string Date, string Description, string Priority, string Assignment)
        {

            bugObj.addBug(Id, Date, Description, Priority, Assignment);
            return RedirectToPage("./Index");
        }
        
    }
}
