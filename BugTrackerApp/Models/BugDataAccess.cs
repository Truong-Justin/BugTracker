using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;


namespace BugTrackerApp.Models
{
    public class BugDataAccess
    {
        string connectionString = "DataSource=Bugs.db";

        public IEnumerable<Bug> GetAllBugs()
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
                    newBug.Description = reader["Description"].ToString();
                    newBug.Priority = Convert.ToString(reader["Priority"]);
                    newBug.Assignment = Convert.ToString(reader["Assignment"]);

                    bugList.Add(newBug);

                }

                connection.Close();

            }

            return bugList;
        }
    }
}

