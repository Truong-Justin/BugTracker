using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Projects
{
	public class ViewProjectModel : PageModel
    {
        public readonly EntityDataAccess _entityDataAccess;
        public readonly PeopleDataAccess _peopleDataAccess;
        public required Project Project { get; set; }
        public ProjectManager ProjectManager { get; set; }
        public Bug Bug { get; set; }
        public IList<Bug> Bugs { get; set; }
        public Employee Employee { get; set; }
        public IList<Employee> Employees { get; set; }

        public ViewProjectModel(EntityDataAccess entityDataAccess, PeopleDataAccess peopleDataAccess)
        {
            _entityDataAccess = entityDataAccess;
            _peopleDataAccess = peopleDataAccess;
        }

        public void OnGet(int id)
        {
            // A specific project record is retrieved
            // from the Projects table, and that project
            // record's project manager id is used to
            // retrieve the project manager that is in
            // charge of that project.
            Project = _entityDataAccess.ViewEntity(id, Project);
            ProjectManager = _peopleDataAccess.ViewPerson(Project.ProjectManagerId, ProjectManager);


            Bugs = _entityDataAccess.GetAllBugsForProject(Project.ProjectId);
            Employees = _entityDataAccess.GetAllEmployeesForProject(Project.ProjectId);

        }

        public ActionResult OnPost(int id)
        {
            _entityDataAccess.DeleteEntity(id, Project);
            return RedirectToPage("/Projects/ProjectIndex");
        }
    }
}
