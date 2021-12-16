using CricketCreationsDatabase;

namespace CricketCreationsRepository.Interfaces
{
    public interface IDatabaseManager
    {
        public CricketCreationsContext Instance { get; set; }
    }
}
