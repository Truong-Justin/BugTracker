using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;




namespace BugTrackerApp.Models
{
    //Class handles the database logic 
    public class BugDataAccess
    {
        readonly private string _connectionString = "DataSource=wwwroot/db/Bugs.db;PRAGMA journal_mode=WAL;";


        //Creates a Bugs database file 
        public void MakeTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
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

        //Returns all record within Bug database
        public List<Bug> GetAllBugs()
        {
            List<Bug> bugList = new List<Bug>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
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

                            bugList.Add(newBug);

                        }
                    }

                }

            }

            return bugList;
        }

        //Adds a new bug record to database
        public void AddBug(int id, DateOnly date, string description, string priority, string assignment)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        INSERT INTO Bugs (Id, Date, Description, Priority, Assignment)
                        VALUES ($Id,$Date,$Description,$Priority,$Assignment)
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

        //deletes a bug from database by id
        public void DeleteBug(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
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

        //Updates bug from database by id
        public void EditBug(int id, DateOnly date, string description, string priority, string assignment)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
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

        //Selects a bug to view more information about
        public Bug ViewBug(int id)
        {
            Bug newBug = new Bug();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
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
    }
}
