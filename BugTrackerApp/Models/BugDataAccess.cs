using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;


namespace BugTrackerApp.Models
{
    public class BugDataAccess
    {
        string connectionString = "DataSource=Bugs.db";

        //returns all record within Bug database
        public List<Bug> GetAllBugs()
        {
            List<Bug> bugList = new List<Bug>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM Bugs;", connection);
                command.CommandType = CommandType.Text;

                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Bug newBug = new Bug();

                    newBug.Id = Convert.ToInt32(reader["Id"]);
                    newBug.Date = Convert.ToString(reader["Date"]);
                    newBug.Description = Convert.ToString(reader["Description"]);
                    newBug.Priority = Convert.ToString(reader["Priority"]);
                    newBug.Assignment = Convert.ToString(reader["Assignment"]);

                    bugList.Add(newBug);

                }

                connection.Close();

            }

            return bugList;
        }

        //adds a new bug record to database
        public void addBug(int Id, string Date, string Description, string Priority, string Assignment)
        {
            using(SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

                SQLiteCommand command = new SQLiteCommand($"INSERT INTO 'Bugs' VALUES('{Id}', '{Date}', '{Description}', '{Priority}', '{Assignment}')", connection);
                command.CommandType = CommandType.Text;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //deletes a bug from database by id
        public void deleteBug(int id)
        {

        }

        //updates bug from database by id
        public void editBug(int id)
        {

        }

        //selects a bug to view more information about
        public void viewBug(int id)
        {
            
           
        }
    }
}

