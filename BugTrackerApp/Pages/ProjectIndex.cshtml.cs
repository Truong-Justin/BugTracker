using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages
{
	public class ProjectIndexModel : PageModel
    {
        EntityDataAccess ObjDataAccess = new EntityDataAccess();
        public IList<Project> Projects { get; set; }
        Project Project { get; set; }

        public void OnGet()
        {
            try
            {
                Projects = ObjDataAccess.GetAllEntities(Project);
                Projects = ObjDataAccess.TruncateDescriptions(Projects);
            }

            catch (Microsoft.Data.Sqlite.SqliteException)
            {
                ObjDataAccess.MakeTable();
                ObjDataAccess.SetJournalMode();
                Projects = ObjDataAccess.GetAllEntities(Project);
                Projects = ObjDataAccess.TruncateDescriptions(Projects);
            }
        }
    }
}
