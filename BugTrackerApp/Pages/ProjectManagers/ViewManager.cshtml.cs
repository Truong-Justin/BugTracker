﻿using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.ProjectManagers
{
	public class ViewManagerModel : PageModel
    {
        public readonly IPeopleDataAccess _peopleDataAccess;
        public required ProjectManager ProjectManager { get; set; }
        public IList<Project> ProjectManagerProjects { get; set; }

        public ViewManagerModel(IPeopleDataAccess peopleDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
        }

        public void OnGet(int id)
        {
            ProjectManager = _peopleDataAccess.ViewPerson(id, ProjectManager);
            ProjectManagerProjects = _peopleDataAccess.GetAllProjectsForManager(ProjectManager.ProjectManagerId);
        }

        public ActionResult OnPost(int id)
        {
            _peopleDataAccess.DeletePerson(id, ProjectManager);
            return RedirectToPage("/ProjectManagers/ProjectManagerIndex");
        }
    }
}
