using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerApp.Models.Entities
{
	public interface IEntityDataAccess
	{
        IList<Bug> GetAllEntities(Bug bug);
        IList<Project> GetAllEntities(Project project);
        IList<Employee> GetAllEmployeesForProject(int projectId);
        IList<Bug> GetAllBugsForProject(int projectId);
        IEnumerable<SelectListItem> GetProjectTitles(IList<Project> Projects);
        void AddEntity(int projectId, DateOnly date, string description, string priority, string assignment, Bug bug);
        void AddEntity(int projectManagerId, DateOnly startDate, string projectTitle, string description, string priority, Project project);
        void DeleteEntity(int id, Bug bug);
        void DeleteEntity(int id, Project project);
        void EditEntity(int id, string description, string priority, string assignment, Bug bug);
        void EditEntity(int id, string projectTitle, string description, string priority, int projectManagerId, Project project);
        Bug ViewEntity(int id, Bug bug);
        Project ViewEntity(int id, Project project);
        IList<Project> TruncateTitles(IList<Project> projects);
        IList<Project> TruncateDescriptions(IList<Project> projects);
        IList<Bug> TruncateDescriptions(IList<Bug> bugs);
    }
}

