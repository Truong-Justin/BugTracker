using System;
using System.Data;
using System.Net;
using System.Numerics;
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
        public IList<ProjectManager> GetAllPeople(ProjectManager projectManager)
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

        // Method returns all employee records
        // from Employees table
        public IList<Employee> GetAllPeople(Employee employee)
        {
            List<Employee> employeesList = new List<Employee>();
            string connectionString = GetConnectionString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = new SqliteCommand("SELECT * FROM Employees", connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee newEmployee = new Employee();

                            newEmployee.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                            newEmployee.FirstName = Convert.ToString(reader["FirstName"]);
                            newEmployee.LastName = Convert.ToString(reader["LastName"]);
                            newEmployee.HireDate = DateOnly.Parse(reader["HireDate"].ToString());
                            newEmployee.Phone = Convert.ToString(reader["Phone"]);
                            newEmployee.Zip = Convert.ToString(reader["Zip"]);
                            newEmployee.Address = Convert.ToString(reader["Address"]);

                            employeesList.Add(newEmployee);
                        }
                    }
                }
            }

            return employeesList;
        }

        //// Method adds a new project manager record
        //// to ProjectManagers table
        public void AddPerson(string firstName, string lastName, DateOnly hireDate, string phone, string zip, string address, ProjectManager projectManager)
        {
            string connectionString = GetConnectionString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        INSERT INTO ProjectManagers (FirstName, LastName, HireDate, Phone, Zip, Address)
                        VALUES ($firstName,$lastName,$hireDate,$phone,$zip,$address)
                    ";

                    command.Parameters.AddWithValue("$firstName", firstName);
                    command.Parameters.AddWithValue("$lastName", lastName);
                    command.Parameters.AddWithValue("$hireDate", hireDate);
                    command.Parameters.AddWithValue("$phone", phone);
                    command.Parameters.AddWithValue("$zip", zip);
                    command.Parameters.AddWithValue("$address", address);

                    command.ExecuteNonQuery();
                }
            }
        }

        //// Method adds a new employee record
        //// to Employees table 
        public void AddPerson(string firstName, string lastName,  DateOnly hireDate, string phone, string zip, string address, Employee employee)
        {
            string connectionString = GetConnectionString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        INSERT INTO Employees (FirstName, LastName, HireDate, Phone, Zip, Address)
                        VALUES ($firstName,$lastName,$hireDate,$phone,$zip,$address)
                     ";

                    command.Parameters.AddWithValue("$firstName", firstName);
                    command.Parameters.AddWithValue("$lastName", lastName);
                    command.Parameters.AddWithValue("$hireDate", hireDate);
                    command.Parameters.AddWithValue("$phone", phone);
                    command.Parameters.AddWithValue("$zip", zip);
                    command.Parameters.AddWithValue("$address", address);

                    command.ExecuteNonQuery();
                }
            }
        }

        //// Method deletes a project manager record
        //// from ProjectManagers table using given Id
        public void DeletePerson(int projectManagerId, ProjectManager projectManager)
        {
            string connectionString = GetConnectionString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        DELETE FROM ProjectManagers
                        WHERE ProjectManagerId = $id
                    ";

                    command.Parameters.AddWithValue("$id", projectManagerId);
                    command.ExecuteNonQuery();
                }
            }
        }

        //// Method deletes an employee record
        //// from Employees table using given Id
        public void DeletePerson(int employeeId, Employee employee)
        {
            string connectionString = GetConnectionString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        DELETE FROM Employees
                        WHERE EmployeeId = $id
                    ";

                    command.Parameters.AddWithValue("$id", employeeId);
                    command.ExecuteNonQuery();
                }
            }
        }

        //// Method updates the fields of a project
        //// manager record using given Id
        public void EditPerson(int projectManagerId, DateOnly hireDate, string firstName, string lastName, string phone, string zip, string address, ProjectManager projectManager)
        {
            string connectionString = GetConnectionString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        UPDATE ProjectManager SET
                        HireDate = $hireDate,
                        FirstName = $firstName,
                        LastName = $lastName,
                        Phone = $phone,
                        Zip = $zip,
                        Address = $address
                        WHERE ProjectManagerID = $id
                    ";

                    command.Parameters.AddWithValue("$hireDate", hireDate);
                    command.Parameters.AddWithValue("$firstName", firstName);
                    command.Parameters.AddWithValue("$lastName", lastName);
                    command.Parameters.AddWithValue("$phone", phone);
                    command.Parameters.AddWithValue("$zip", zip);
                    command.Parameters.AddWithValue("address", address);
                    command.Parameters.AddWithValue("projectManagerId", projectManagerId);
                }
            }
        }

        //// Method updates the fields of an employee
        //// record using given Id
        public void EditPerson(int employeeId, DateOnly hireDate, string firstName, string lastName, string phone, string zip, string address, Employee employee)
        {
            string connectionString = GetConnectionString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        UPDATE Employees SET
                        HireDate = $hireDate,
                        FirstName = $firstName,
                        LastName = $lastName,
                        Phone = $phone,
                        Zip = $zip,
                        Address = $address
                        WHERE EmployeeId = $employeeId
                    ";

                    command.Parameters.AddWithValue("$hireDate", hireDate);
                    command.Parameters.AddWithValue("$firstName", firstName);
                    command.Parameters.AddWithValue("$lastName", lastName);
                    command.Parameters.AddWithValue("$phone", phone);
                    command.Parameters.AddWithValue("$zip", zip);
                    command.Parameters.AddWithValue("address", address);
                    command.Parameters.AddWithValue("employeeId", employeeId);
                }
            }
        }

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
                        WHERE ProjectManagerId = $projectManagerId
                    ";

                    command.Parameters.AddWithValue("$projectManagerId", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            newManager.ProjectManagerId = Convert.ToInt32(reader["ProjectManagerId"]);
                            newManager.FirstName = Convert.ToString(reader["FirstName"]);
                            newManager.LastName = Convert.ToString(reader["LastName"]);
                            newManager.HireDate = DateOnly.Parse(reader["HireDate"].ToString());
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
        //// record using given Id
        public Employee ViewPerson(int employeeId, Employee employee)
        {
            Employee newEmployee = new Employee();
            string connectionString = GetConnectionString();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        SELECT * FROM Employees
                        WHERE EmployeeId = $employeeId
                    ";

                    command.Parameters.AddWithValue("$employeeId", employeeId);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            newEmployee.FirstName = Convert.ToString(reader["FirstName"]);
                            newEmployee.LastName = Convert.ToString(reader["LastName"]);
                            newEmployee.HireDate = DateOnly.Parse(reader["HireDate"].ToString());
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

