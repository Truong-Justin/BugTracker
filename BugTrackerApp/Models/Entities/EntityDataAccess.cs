using System;
using System.Data;
using Microsoft.Data.Sqlite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BugTrackerApp.Models
{
    public class EntityDataAccess
    {

        public void MakeTable()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = new SqliteCommand(
                        "BEGIN TRANSACTION;" +
                        "CREATE TABLE `Bugs` (`Id` NUMERIC PRIMARY KEY,`Date` varchar(10),`Description` varchar(125),`Priority` varchar(10),`Assignment` varchar(10));" +
                        "INSERT INTO 'Bugs' VALUES('1', '2022-01-08', 'This bug is making the web app crash', 'High', 'Luke');" +
                        "INSERT INTO 'Bugs' VALUES('2', '2022-01-15', 'A bug is making the JS animation script to not work when the page is loaded', 'Low', 'Lucy');" +
                         "COMMIT;", connection))
                {
                    command.CommandType = CommandType.Text;

                    command.ExecuteNonQuery();
                }
            }
        }

        // Methode sets journal mode to write-ahead-logging to simulate
        // concurrent read/write access; SQLite does not support concurrent
        // read/write access natively so WAL is used to write to the database when
        // a new database connection can be made 
        public void SetJournalMode()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = new SqliteCommand("PRAGMA journal_mode=WAL;", connection))
                {
                    command.CommandType = CommandType.Text;

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method returns all bug records from Bugs table
        public IList<Bug> GetAllEntities(Bug bug)
        {
            List<Bug> bugsList = new List<Bug>();
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = new SqliteCommand("SELECT * FROM Bugs;", connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bug newBug = new Bug();

                            newBug.BugId = Convert.ToInt32(reader["BugId"]);
                            newBug.Date = DateOnly.Parse(reader["Date"].ToString());
                            newBug.Description = Convert.ToString(reader["Description"]);
                            newBug.Priority = Convert.ToString(reader["Priority"]);
                            newBug.Assignment = Convert.ToString(reader["Assignment"]);

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
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = new SqliteCommand("SELECT * FROM Projects;", connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Project newProject = new Project();

                            newProject.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            newProject.Date = DateOnly.Parse(reader["StartDate"].ToString());
                            newProject.ProjectTitle = Convert.ToString(reader["ProjectTitle"]);
                            newProject.Description = Convert.ToString(reader["Description"]);
                            newProject.Priority = Convert.ToString(reader["Priority"]);
                            newProject.ProjectManagerId = Convert.ToInt32(reader["ProjectManagerId"]);

                            projectsList.Add(newProject);
                        }
                    }
                }
            }

            return projectsList.AsReadOnly();
        }

        // Method adds a new bug record to the Bugs table
        public void AddEntity(int projectId, DateOnly date, string description, string priority, string assignment, Bug bug)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        INSERT INTO Bugs (ProjectId, Date, Description, Priority, Assignment)
                        VALUES ($projectId,$date,$description,$priority,$assignment)
                    ";

                    command.Parameters.AddWithValue("$projectId", projectId);
                    command.Parameters.AddWithValue("$date", date);
                    command.Parameters.AddWithValue("$description", description);
                    command.Parameters.AddWithValue("$priority", priority);
                    command.Parameters.AddWithValue("$assignment", assignment);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method adds a new project record to the Projects table
        public void AddEntity(int projectManagerId, DateOnly startDate, string projectTitle, string description, string priority, Project project)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        INSERT INTO Projects (ProjectManagerId, StartDate, ProjectTitle, Description, Priority)
                        VALUES ($projectManagerId,$startDate,$projectTitle,$description,$priority)
                    ";

                    command.Parameters.AddWithValue("$projectManagerId", projectManagerId);
                    command.Parameters.AddWithValue("$startDate", startDate);
                    command.Parameters.AddWithValue("$projectTitle", projectTitle);
                    command.Parameters.AddWithValue("$description", description);
                    command.Parameters.AddWithValue("$priority", priority);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method deletes a bug record from the Bugs table
        // using the given Id
        public void DeleteEntity(int id, Bug bug)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        DELETE FROM BUGS
                        WHERE BugId = $id
                    ";

                    command.Parameters.AddWithValue("$id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Method deletes a project record from the Projects table
        // using the given Id
        public void DeleteEntity(int id, Project project)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        DELETE FROM PROJECTS
                        WHERE ProjectId = $id
                    ";

                    command.Parameters.AddWithValue("$id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Method updates the fields of a bug record from the Bugs table
        // using the given Id
        public void EditEntity(int id, DateOnly date, string description, string priority, string assignment, Bug bug)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        UPDATE Bugs SET
                        Date = $date,
                        Description = $description,
                        Priority = $priority,
                        Assignment = $assignment
                        WHERE BugId = $id
                    ";

                    command.Parameters.AddWithValue("$date", date);
                    command.Parameters.AddWithValue("$description", description);
                    command.Parameters.AddWithValue("$priority", priority);
                    command.Parameters.AddWithValue("$assignment", assignment);
                    command.Parameters.AddWithValue("$id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method updates the fields of a bug record from the Projects table
        // using the given Id
        public void EditEntity(int id, DateOnly date, string description, string priority, string assignment, Project project)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        UPDATE Projects SET
                        Date = $date,
                        Description = $description,
                        Priority = $priority,
                        Assignment = $assignment
                        WHERE ProjectId = $id
                    ";

                    command.Parameters.AddWithValue("$date", date);
                    command.Parameters.AddWithValue("$description", description);
                    command.Parameters.AddWithValue("$priority", priority);
                    command.Parameters.AddWithValue("$assignment", assignment);
                    command.Parameters.AddWithValue("$id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method selects a specific bug record from the Bugs table
        // using the given Id
        public Bug ViewEntity(int id, Bug bug)
        {
            Bug newBug = new Bug();
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        SELECT *
                        FROM BUGS
                        WHERE BugId = $id
                    ";

                    command.Parameters.AddWithValue("$id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            newBug.BugId = Convert.ToInt32(reader["BugId"]);
                            newBug.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            newBug.Date = DateOnly.Parse(reader["Date"].ToString());
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
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        SELECT *
                        FROM PROJECTS
                        WHERE ProjectId = $id
                    ";

                    command.Parameters.AddWithValue("$id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            newProject.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            newProject.Date = DateOnly.Parse(reader["StartDate"].ToString());
                            newProject.ProjectTitle = Convert.ToString(reader["ProjectTitle"]);
                            newProject.Description = Convert.ToString(reader["Description"]);
                            newProject.Priority = Convert.ToString(reader["Priority"]);
                        }
                    }
                }
            }

            return newProject;
        }

        // Method truncates description to prevent the UI from breaking
        public IList<Bug> TruncateDescriptions(IList<Bug> bugs)
        {
            foreach (Bug bug in bugs.Where(bug => bug.Description.Length > 80))
            {
                bug.Description = bug.Description.Substring(0, 80) + "...";
            }

            return bugs;
        }

        // Method truncates description to prevent the UI from breaking
        public IList<Project> TruncateDescriptions(IList<Project> projects)
        {
            foreach (Project project in projects.Where(project => project.Description.Length > 80))
            {
                project.Description = project.Description.Substring(0, 80) + "...";
            }

            return projects;
        }

    }
}

