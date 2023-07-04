using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace BugTrackerApp.Models.People
{

    public class PeopleDataAccess
	{

        public string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("SQLiteDb");

            return connectionString;
        }

        // Method returns all project manager records
        // from ProjectManagers table
        public IList<ProjectManager> GetAllEntities(ProjectManager projectManager)
        {
            List<ProjectManager> projectManagersList = new List<ProjectManager>();
            string connectionString = GetConnectionString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = new SqliteCommand("SELECT * FROM ProjectManagers;", connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProjectManager newProjectManager = new ProjectManager();

                            newProjectManager.ProjectManagerId = Convert.ToInt32(reader["ProjectManagerId"]);
                            newProjectManager.FirstName = Convert.ToString(reader["FirstName"]);
                            newProjectManager.LastName = Convert.ToString(reader["LastName"]);
                            newProjectManager.HireDate = DateOnly.Parse(reader["HireDate"].ToString());

                            projectManagersList.Add(newProjectManager);
                        }
                    }
                }
            }

            return projectManagersList.AsReadOnly();
        }

        // Method returns all employee records
        // from Employees table
        //public IList<Employee> GetAllPeople(Employee employee)
        //{

        //}

        //// Method adds a new project manager record
        //// to ProjectManagers table
        //public void AddPerson(int id, string firstName, string lastName, DateOnly hireDate, ProjectManager projectManager)
        //{

        //}

        //// Method adds a new employee record
        //// to Employees table 
        //public void AddPerson(int id, string firstName, string lastName, string employeeType, DateOnly hireDate, Employee employee)
        //{

        //}

        //// Method deletes a project manager record
        //// from ProjectManagers table using given Id
        //public void DeletePerson(int id, ProjectManager projectManager)
        //{

        //}

        //// Method deletes an employee record
        //// from Employees table using given Id
        //public void DeletePerson(int id, Employee employee)
        //{

        //}

        //// Method updates the fields of a project
        //// manager record using given Id
        //public void EditPerson()
        //{

        //}

        //// Method updates the fields of an employee
        //// record using given Id
        //public void EditPerson()
        //{

        //}

        // Method selects a specific project manager
        // record using given Id
        public ProjectManager ViewPerson(int id, ProjectManager ProjectManager)
        {
            ProjectManager newManager = new ProjectManager();
            string connectionString = GetConnectionString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        SELECT *
                        FROM PROJECTMANAGERS
                        WHERE ProjectManagerId = $id
                    ";

                    command.Parameters.AddWithValue("$id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            newManager.ProjectManagerId = Convert.ToInt32(reader["ProjectManagerId"]);
                            newManager.FirstName = Convert.ToString(reader["FirstName"]);
                            newManager.LastName = Convert.ToString(reader["LastName"]);
                            newManager.HireDate = DateOnly.Parse(reader["HireDate"].ToString());
                        }
                    }
                }
            }

            return newManager;
        }

        //// Method selects a specific employee
        //// record using given Id
        //public Employee ViewPerson()
        //{

        //}
    }
}

