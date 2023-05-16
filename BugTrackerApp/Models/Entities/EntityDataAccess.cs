using System;
using System.Data;
using System.Data.SQLite;

namespace BugTrackerApp.Models
{
    // Abstract class made to support inheritance,
    // code-reusability, and method overloading for future
    // subclasses
    public abstract class EntityDataAccess
    {

        public void MakeTable()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(
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

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("PRAGMA journal_mode=WAL;", connection))
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

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Bugs;", connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bug newBug = new Bug();

                            newBug.Id = Convert.ToInt32(reader["Id"]);
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

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Projects;", connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Project newProject = new Project();

                            newProject.Id = Convert.ToInt32(reader["Id"]);
                            newProject.Date = DateOnly.Parse(reader["Date"].ToString());
                            newProject.Description = Convert.ToString(reader["Description"]);
                            newProject.Priority = Convert.ToString(reader["Priority"]);
                            newProject.Assignment = Convert.ToString(reader["Assignment"]);

                            projectsList.Add(newProject);
                        }
                    }
                }
            }

            return projectsList.AsReadOnly();
        }

        // Method adds a new bug record to the Bugs table
        public void AddEntity(int id, DateOnly date, string description, string priority, string assignment, Bug bug)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        INSERT INTO Bugs (Id, Date, Description, Priority, Assignment)
                        VALUES ($id,$date,$description,$priority,$assignment)
                    ";

                    command.Parameters.AddWithValue("$id", id);
                    command.Parameters.AddWithValue("$date", date);
                    command.Parameters.AddWithValue("$description", description);
                    command.Parameters.AddWithValue("$priority", priority);
                    command.Parameters.AddWithValue("$assignment", assignment);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Method adds a new project record to the Projects table
        public void AddEntity(int id, DateOnly date, string description, string priority, string assignment, Project project)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("SQLiteDb");

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        INSERT INTO Projects (Id, Date, Description, Priority, Assignment)
                        VALUES ($id,$date,$description,$priority,$assignment)
                    ";

                    command.Parameters.AddWithValue("$id", id);
                    command.Parameters.AddWithValue("$date", date);
                    command.Parameters.AddWithValue("$description", description);
                    command.Parameters.AddWithValue("$priority", priority);
                    command.Parameters.AddWithValue("$assignment", assignment);

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

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        DELETE FROM BUGS
                        WHERE Id = $id
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

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        DELETE FROM BUGS
                        WHERE Id = $id
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

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        UPDATE Bugs SET
                        Date = $date,
                        Description = $description,
                        Priority = $priority,
                        Assignment = $assignment
                        WHERE Id = $id
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

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        UPDATE Projects SET
                        Date = $date,
                        Description = $description,
                        Priority = $priority,
                        Assignment = $assignment
                        WHERE Id = $id
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

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        SELECT *
                        FROM BUGS
                        WHERE Id = $id
                    ";

                    command.Parameters.AddWithValue("$id", id);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            newBug.Id = Convert.ToInt32(reader["Id"]);
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

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        SELECT *
                        FROM PROJECTS
                        WHERE Id = $id
                    ";

                    command.Parameters.AddWithValue("$id", id);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            newProject.Id = Convert.ToInt32(reader["Id"]);
                            newProject.Date = DateOnly.Parse(reader["Date"].ToString());
                            newProject.Description = Convert.ToString(reader["Description"]);
                            newProject.Priority = Convert.ToString(reader["Priority"]);
                            newProject.Assignment = Convert.ToString(reader["Assignment"]);
                        }
                    }
                }
            }

            return newProject;
        }

    }
}

