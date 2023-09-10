using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace BugTrackerApp.Models
{
    public class EntityDataAccess
    {
        private readonly string _connectionString;

        public EntityDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CONNECTION");
        }

        // Method returns all bug records from Bugs table
        public IList<Bug> GetAllEntities(Bug bug)
        {
            List<Bug> bugsList = new List<Bug>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Bugs;", connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bug newBug = new Bug();

                            newBug.BugId = Convert.ToInt32(reader["BugId"]);
                            newBug.Date = DateOnly.FromDateTime((DateTime)reader["Date"]);
                            newBug.Description = Convert.ToString(reader["Description"]);
                            newBug.Priority = Convert.ToString(reader["Priority"]);
                            newBug.Assignment = Convert.ToString(reader["Assignment"]);
                            newBug.ProjectId = Convert.ToInt32(reader["ProjectId"]);

                            bugsList.Add(newBug);
                        }
                    }
                }
            }

            return bugsList.AsReadOnly();
        }

        // Method returns all project records from Projects table
        public IList<Project> GetAllEntities(Project project)
        {
            List<Project> projectsList = new List<Project>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Projects;", connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Project newProject = new Project();

                            newProject.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            newProject.Date = DateOnly.FromDateTime((DateTime)reader["StartDate"]);
                            newProject.ProjectTitle = Convert.ToString(reader["ProjectTitle"]);
                            newProject.Description = Convert.ToString(reader["Description"]);
                            newProject.Priority = Convert.ToString(reader["Priority"]);

                            projectsList.Add(newProject);
                        }
                    }
                }
            }

            return projectsList.AsReadOnly();
        }

        // Converts the list of Projects to
        // a collection of SelectListItem objects
        // used to populate the asp-items for project assignment
        public IEnumerable<SelectListItem> GetProjectTitles(IList<Project> Projects)
        {
            return Projects.Select(project => new SelectListItem { Value = project.ProjectId.ToString(), Text = project.ProjectTitle });
        }

        // Method adds a new bug record to the Bugs table
        public void AddEntity(int projectId, DateOnly date, string description, string priority, string assignment, Bug bug)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        INSERT INTO Bugs (ProjectId, Date, Description, Priority, Assignment)
                        VALUES (@projectId, @date, @description, @priority, @assignment)
                    ";

                    command.Parameters.AddWithValue("@projectId", projectId);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@priority", priority);
                    command.Parameters.AddWithValue("@assignment", assignment);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method adds a new project record to the Projects table
        public void AddEntity(int projectManagerId, DateOnly startDate, string projectTitle, string description, string priority, Project project)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        INSERT INTO Projects (ProjectManagerId, StartDate, ProjectTitle, Description, Priority)
                        VALUES (@projectManagerId, @startDate, @projectTitle, @description, @priority)
                    ";

                    command.Parameters.AddWithValue("@projectManagerId", projectManagerId);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@projectTitle", projectTitle);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@priority", priority);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method deletes a bug record from the Bugs table
        // using the given Id
        public void DeleteEntity(int id, Bug bug)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        DELETE FROM BUGS
                        WHERE BugId = @id
                    ";

                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Method deletes a project record from the Projects table
        // using the given Id
        public void DeleteEntity(int id, Project project)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        DELETE FROM PROJECTS
                        WHERE ProjectId = @id
                    ";

                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Method updates the fields of a bug record from the Bugs table
        // using the given Id
        public void EditEntity(int id, DateOnly date, string description, string priority, string assignment, Bug bug)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        UPDATE Bugs SET
                        Date = @date,
                        Description = @description,
                        Priority = @priority,
                        Assignment = @assignment
                        WHERE BugId = @id
                    ";

                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@priority", priority);
                    command.Parameters.AddWithValue("@assignment", assignment);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method updates the fields of a project record from the Projects table
        // using the given Id
        public void EditEntity(int id, string projectTitle, string description, string priority, int projectManagerId, Project project)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        UPDATE Projects SET
                        ProjectTitle = @projectTitle,
                        Description = @description,
                        Priority = @priority,
                        ProjectManagerId = @projectManagerId
                        WHERE ProjectId = @id
                    ";

                    command.Parameters.AddWithValue("@projectTitle", projectTitle);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@priority", priority);
                    command.Parameters.AddWithValue("@projectManagerId", projectManagerId);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method selects a specific bug record from the Bugs table
        // using the given Id
        public Bug ViewEntity(int id, Bug bug)
        {
            Bug newBug = new Bug();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        SELECT *
                        FROM BUGS
                        WHERE BugId = @id
                    ";

                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            newBug.BugId = Convert.ToInt32(reader["BugId"]);
                            newBug.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            newBug.Date = DateOnly.FromDateTime((DateTime)reader["Date"]);
                            newBug.Description = Convert.ToString(reader["Description"]);
                            newBug.Priority = Convert.ToString(reader["Priority"]);
                            newBug.Assignment = Convert.ToString(reader["Assignment"]);
                        }
                    }
                }
            }

            return newBug;
        }

        // Method selects a specific project from the Projects table
        // using the given Id
        public Project ViewEntity(int id, Project project)
        {
            Project newProject = new Project();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        SELECT *
                        FROM PROJECTS
                        WHERE ProjectId = @id
                    ";

                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            newProject.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            newProject.Date = DateOnly.FromDateTime((DateTime)reader["StartDate"]);
                            newProject.ProjectTitle = Convert.ToString(reader["ProjectTitle"]);
                            newProject.Description = Convert.ToString(reader["Description"]);
                            newProject.Priority = Convert.ToString(reader["Priority"]);
                            newProject.ProjectManagerId = Convert.ToInt32(reader["ProjectManagerId"]);
                        }
                    }
                }
            }

            return newProject;
        }

        // Method truncates the project titles so that the card containers will all look uniform
        public IList<Project> TruncateTitles(IList<Project> projects)
        {
            foreach (Project project in projects.Where(project => project.ProjectTitle.Length > 40))
            {
                project.ProjectTitle = project.ProjectTitle.Substring(0, 40) + "...";
            }

            return projects;
        }

        // Method truncates bug description to keep the card containers looking uniform
        public IList<Bug> TruncateDescriptions(IList<Bug> bugs)
        {
            foreach (Bug bug in bugs.Where(bug => bug.Description.Length > 70))
            {
                bug.Description = bug.Description.Substring(0, 70) + "...";
            }

            return bugs;
        }

        // Method truncates project description to keep the card containers looking uniform
        public IList<Project> TruncateDescriptions(IList<Project> projects)
        {
            foreach (Project project in projects.Where(project => project.Description.Length > 70))
            {
                project.Description = project.Description.Substring(0, 70) + "...";
            }

            return projects;
        }
    }
}

