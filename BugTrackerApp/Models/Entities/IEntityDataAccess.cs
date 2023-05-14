using System;
namespace BugTrackerApp.Models
{
    // Interface created to allow for extensibility
    // and loose coupling as more tables will be added
    // and accessed from
    public interface IEntityDataAccess
    {
        void MakeTable();

        IList<Bug> GetAllEntities();

        void AddEntity(int id, DateOnly date, string description, string priority, string assignment);

        void DeleteEntity(int d);

        void EditEntity(int id, DateOnly date, string description, string priority, string assignment);

        Object ViewEntity(int id);

    }
}

