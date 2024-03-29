﻿using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace BugTrackerApp.Models.People
{

    public class PeopleDataAccess : IPeopleDataAccess
	{
        private readonly string _connectionString;

        // IConfiguration service is dependency injected into class constructor
        // so that the configuration object can retrieve the connection string
        // from host environment variable.
        public PeopleDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CONNECTION");
        }

        // Method returns all project manager records
        // from ProjectManagers table.
        public IList<ProjectManager> GetAllPeople(ProjectManager projectManager)
        {
            List<ProjectManager> projectManagersList = new List<ProjectManager>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM ProjectManagers;", connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProjectManager newProjectManager = new ProjectManager();

                            newProjectManager.ProjectManagerId = Convert.ToInt32(reader["ProjectManagerId"]);
                            newProjectManager.FirstName = Convert.ToString(reader["FirstName"]);
                            newProjectManager.LastName = Convert.ToString(reader["LastName"]);
                            newProjectManager.HireDate = DateOnly.FromDateTime((DateTime)reader["HireDate"]);
                            newProjectManager.Phone = Convert.ToString(reader["Phone"]);
                            newProjectManager.Zip = Convert.ToString(reader["Zip"]);
                            newProjectManager.Address = Convert.ToString(reader["Address"]);

                            projectManagersList.Add(newProjectManager);
                        }
                    }
                }
            }

            return projectManagersList.AsReadOnly();
        }

        // Method returns all the projects a Project Manager
        // is currently in charge of using an Inner Join
        // between the ProjectManagers and Projects tables.
        public IList<Project> GetAllProjectsForManager(int projectManagerId)
        {
            List<Project> projectsList = new List<Project>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        SELECT Projects.ProjectTitle, Projects.ProjectId
                        FROM Projects INNER JOIN ProjectManagers
                        ON Projects.ProjectManagerId = ProjectManagers.ProjectManagerId
                        WHERE Projects.ProjectManagerId = @projectManagerId
                    ";

                    command.Parameters.AddWithValue("@projectManagerId", projectManagerId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Project newProject = new Project();

                            newProject.ProjectTitle = Convert.ToString(reader["ProjectTitle"]);
                            newProject.ProjectId = Convert.ToInt32(reader["ProjectId"]);

                            projectsList.Add(newProject);
                        }
                    }
                }
            }

            return projectsList.AsReadOnly();
        }

        // Method returns all employee records
        // from Employees table.
        public IList<Employee> GetAllPeople(Employee employee)
        {
            List<Employee> employeesList = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Employees", connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee newEmployee = new Employee();

                            newEmployee.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                            newEmployee.FirstName = Convert.ToString(reader["FirstName"]);
                            newEmployee.LastName = Convert.ToString(reader["LastName"]);
                            newEmployee.HireDate = DateOnly.FromDateTime((DateTime)reader["HireDate"]);
                            newEmployee.Phone = Convert.ToString(reader["Phone"]);
                            newEmployee.Zip = Convert.ToString(reader["Zip"]);
                            newEmployee.Address = Convert.ToString(reader["Address"]);

                            employeesList.Add(newEmployee);
                        }
                    }
                }
            }

            return employeesList.AsReadOnly();
        }

        // Converts the list of Project Managers to
        // a collection of SelectListItem objects used 
        // to populate the asp-items for project manager.
        public IEnumerable<SelectListItem> GetProjectManagerNames(IList<ProjectManager> projectManagers)
        {
            return projectManagers.Select(pm => new SelectListItem { Value = pm.ProjectManagerId.ToString(), Text = pm.FirstName + " " + pm.LastName });
        }

        // Converts the list of Employees to a collection
        // of SelectListItem objects used to populate the
        // asp-items for employee names.
        public IEnumerable<SelectListItem> GetEmployeeNames(IList<Employee> employees)
        {
            return employees.Select(employee => new SelectListItem { Value = employee.FirstName.ToString() + " " + employee.LastName, Text = employee.FirstName + " " + employee.LastName });
        }

        //// Method adds a new project manager record
        //// to ProjectManagers table.
        public void AddPerson(string firstName, string lastName, DateOnly hireDate, string phone, string zip, string address, ProjectManager projectManager)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        INSERT INTO ProjectManagers (FirstName, LastName, HireDate, Phone, Zip, Address)
                        VALUES (@firstName, @lastName, @hireDate, @phone, @zip, @address)
                    ";

                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@hireDate", hireDate);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@zip", zip);
                    command.Parameters.AddWithValue("@address", address);

                    command.ExecuteNonQuery();
                }
            }
        }

        //// Method adds a new employee record
        //// to Employees table.
        public void AddPerson(string firstName, string lastName,  DateOnly hireDate, string phone, string zip, string address, int projectId, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        INSERT INTO Employees (FirstName, LastName, HireDate, Phone, Zip, Address, ProjectId)
                        VALUES (@firstName, @lastName, @hireDate, @phone, @zip, @address, @projectId)
                     ";

                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@hireDate", hireDate);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@zip", zip);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("projectId", projectId);

                    command.ExecuteNonQuery();
                }
            }
        }

        //// Method deletes a project manager record
        //// from ProjectManagers table using given Id.
        public void DeletePerson(int projectManagerId, ProjectManager projectManager)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        DELETE FROM ProjectManagers
                        WHERE ProjectManagerId = @id
                    ";

                    command.Parameters.AddWithValue("@id", projectManagerId);
                    command.ExecuteNonQuery();
                }
            }
        }

        //// Method deletes an employee record
        //// from Employees table using given Id.
        public void DeletePerson(int employeeId, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        DELETE FROM Employees
                        WHERE EmployeeId = @id
                    ";

                    command.Parameters.AddWithValue("@id", employeeId);
                    command.ExecuteNonQuery();
                }
            }
        }

        //// Method updates the fields of a project
        //// manager record using given Id.
        public void EditPerson(int projectManagerId, string phone, string zip, string address, ProjectManager projectManager)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        UPDATE ProjectManagers SET
                        Phone = @phone,
                        Zip = @zip,
                        Address = @address
                        WHERE ProjectManagerID = @projectManagerId
                    ";

                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@zip", zip);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@projectManagerId", projectManagerId);

                    command.ExecuteNonQuery();
                }
            }
        }

        //// Method updates the fields of an employee
        //// record using given Id.
        public void EditPerson(int employeeId, string phone, string zip, string address, int projectId, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        UPDATE Employees SET
                        ProjectId = @projectId,
                        Phone = @phone,
                        Zip = @zip,
                        Address = @address
                        WHERE EmployeeId = @employeeId
                    ";

                    command.Parameters.AddWithValue("@projectId", projectId);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@zip", zip);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@employeeId", employeeId);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method selects a specific project manager
        // record using given Id.
        public ProjectManager ViewPerson(int projectManagerId, ProjectManager ProjectManager)
        {
            ProjectManager newManager = new ProjectManager();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        SELECT *
                        FROM PROJECTMANAGERS
                        WHERE ProjectManagerId = @projectManagerId
                    ";

                    command.Parameters.AddWithValue("@projectManagerId", projectManagerId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            newManager.ProjectManagerId = Convert.ToInt32(reader["ProjectManagerId"]);
                            newManager.FirstName = Convert.ToString(reader["FirstName"]);
                            newManager.LastName = Convert.ToString(reader["LastName"]);
                            newManager.HireDate = DateOnly.FromDateTime((DateTime)reader["HireDate"]);
                            newManager.Phone = Convert.ToString(reader["Phone"]);
                            newManager.Zip = Convert.ToString(reader["Zip"]);
                            newManager.Address = Convert.ToString(reader["Address"]);
                        }
                    }
                }
            }

            return newManager;
        }

        //// Method selects a specific employee
        //// record using given Id.
        public Employee ViewPerson(int employeeId, Employee employee)
        {
            Employee newEmployee = new Employee();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        SELECT * FROM Employees
                        WHERE EmployeeId = @employeeId
                    ";

                    command.Parameters.AddWithValue("@employeeId", employeeId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            newEmployee.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                            newEmployee.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            newEmployee.FirstName = Convert.ToString(reader["FirstName"]);
                            newEmployee.LastName = Convert.ToString(reader["LastName"]);
                            newEmployee.HireDate = DateOnly.FromDateTime((DateTime)reader["HireDate"]);
                            newEmployee.Phone = Convert.ToString(reader["Phone"]);
                            newEmployee.Zip = Convert.ToString(reader["Zip"]);
                            newEmployee.Address = Convert.ToString(reader["Address"]);
                        }
                    }
                }
            }

            return newEmployee;
        }
    }
}

