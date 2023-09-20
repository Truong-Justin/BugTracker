﻿using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BugTrackerApp.Pages.Projects
{
	public class EditProjectModel : PageModel
    {
        public readonly EntityDataAccess _entityDataAccess;
        public readonly PeopleDataAccess _peopleDataAccess;
        [BindProperty]
        public required Project Project { get; set; }
        public required ProjectManager ProjectManager { get; set; }
        public required IList<ProjectManager> ProjectManagers { get; set; }


        public EditProjectModel(EntityDataAccess entityDataAccess, PeopleDataAccess peopleDataAccess)
        {
            _entityDataAccess = entityDataAccess;
            _peopleDataAccess = peopleDataAccess;
        }


        public IActionResult OnGet(int id)
        {
            Project = _entityDataAccess.ViewEntity(id, Project);
            ProjectManagers = _peopleDataAccess.GetAllPeople(ProjectManager);
            return Page();
        }

        // Populate the form fields with the attributes of the selected
        // bug the user wants to update.
        // When the user enters new data into the form,
        // the bug record is updated with new values.
        public IActionResult OnPost(int id, string projectTitle, string description, string priority, int projectManagerId)
        {
            projectTitle = Project.ProjectTitle;
            description = Project.Description;
            priority = Project.Priority;
            projectManagerId = Project.ProjectManagerId;

            _entityDataAccess.EditEntity(id, projectTitle, description, priority, projectManagerId, Project);
            return RedirectToPage("./ProjectIndex");
        }
    }
}
