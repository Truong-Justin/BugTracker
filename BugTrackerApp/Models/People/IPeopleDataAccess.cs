
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerApp.Models.People
{
	public interface IPeopleDataAccess
	{
        IList<ProjectManager> GetAllPeople(ProjectManager projectManager);
        IList<Employee> GetAllPeople(Employee employee);
        IList<Project> GetAllProjectsForManager(int projectManagerId);
        IEnumerable<SelectListItem> GetProjectManagerNames(IList<ProjectManager> projectManagers);
        IEnumerable<SelectListItem> GetEmployeeNames(IList<Employee> employees);
        void AddPerson(string firstName, string lastName, DateOnly hireDate, string phone, string zip, string address, ProjectManager projectManager);
        void AddPerson(string firstName, string lastName, DateOnly hireDate, string phone, string zip, string address, int projectId, Employee employee);
        void DeletePerson(int projectManagerId, ProjectManager projectManager);
        void DeletePerson(int employeeId, Employee employee);
        void EditPerson(int projectManagerId, string phone, string zip, string address, ProjectManager projectManager);
        void EditPerson(int employeeId, string phone, string zip, string address, int projectId, Employee employee);
        ProjectManager ViewPerson(int projectManagerId, ProjectManager ProjectManager);
        Employee ViewPerson(int employeeId, Employee employee);
    }
}

