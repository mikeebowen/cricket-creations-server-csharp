using System.Collections.Generic;
using System.Linq;
using CricketCreationsDatabase;
using CricketCreationsRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CricketCreationsRepository
{
    public class DatabaseManager : IDatabaseManager
    {
        public DatabaseManager()
        {
            Instance = new CricketCreationsContext();

            Instance.SaveChangesFailed += _handleFailedSave;
        }

        public CricketCreationsContext Instance { get; set; }

        private void _handleFailedSave(object sender, SaveChangesFailedEventArgs saveChangesFailedEventArgs)
        {
            List<EntityEntry> changedEntries = Instance.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted).ToList();

            foreach (EntityEntry entry in changedEntries)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
