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
        string connectionString = "DataSource=wwwroot/db/Bugs.db;journal mode=On;";


        //Creates a Bugs database file 
        public void makeTable()
        {
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

        //Returns all record within Bug database
        public List<Bug> GetAllBugs()
        {
            List<Bug> bugList = new List<Bug>();

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

                            bugList.Add(newBug);

                        }
                    }

                }

            }

            return bugList;
        }

        //Adds a new bug record to database
        public void addBug(int Id, DateOnly Date, string Description, string Priority, string Assignment)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        INSERT INTO Bugs (Id, Date, Description, Priority, Assignment)
                        VALUES ($Id,$Date,$Description,$Priority,$Assignment)
                    ";

                    command.Parameters.AddWithValue("$Id", Id);
                    command.Parameters.AddWithValue("$Date", Date);
                    command.Parameters.AddWithValue("$Description", Description);
                    command.Parameters.AddWithValue("$Priority", Priority);
                    command.Parameters.AddWithValue("$Assignment", Assignment);

                    command.ExecuteNonQuery();
                }
            }
        }

        //deletes a bug from database by id
        public void deleteBug(int Id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        DELETE FROM BUGS
                        WHERE Id = $Id
                    ";

                    command.Parameters.AddWithValue("$id", Id);

                    command.ExecuteNonQuery();
                }
            }
            
        }

        //Updates bug from database by id
        public void editBug(int Id, DateOnly Date, string Description, string Priority, string Assignment)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        UPDATE Bugs SET
                        Date = $Date,
                        Description = $Description,
                        Priority = $Priority,
                        Assignment = $Assignment
                        WHERE Id = $Id
                    ";

                    command.Parameters.AddWithValue("$Date", Date);
                    command.Parameters.AddWithValue("$Description", Description);
                    command.Parameters.AddWithValue("$Priority", Priority);
                    command.Parameters.AddWithValue("$Assignment", Assignment);
                    command.Parameters.AddWithValue("$Id", Id);

                    command.ExecuteNonQuery();
                    
                }
            }
        }

        //Selects a bug to view more information about
        public Bug viewBug(int Id)
        {
            Bug newBug = new Bug();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                    @"
                        SELECT *
                        FROM BUGS
                        WHERE Id = $Id
                    ";

                    command.Parameters.AddWithValue("$Id", Id);

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
