using RankingApp.Data.Models;

namespace RankingApp.Data.Service
{
    public class RankingService
    {
        ApplicationDbContext _context;

        public RankingService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        public Task<GameResult> AddGameResult(GameResult gameResult)
        {
            _context.GameResults.Add(gameResult);
            _context.SaveChanges();

            return Task.FromResult(gameResult);
        }

        // Read
        public Task<List<GameResult>> GetGameResultsAsync()
        {
            List<GameResult> results = _context.GameResults
                .OrderByDescending(item => item.Score)
                .ToList();

            return Task.FromResult(results);
        }

        // Update
        public Task<bool> UpdateGameResult(GameResult gameResult)
        {
            var findResult = _context.GameResults
                .Where(x=>x.Id == gameResult.Id)
                .FirstOrDefault();

            if (findResult == null)
            {
                return Task.FromResult(false);
            }

            findResult.USerName = gameResult.USerName;
            findResult.Score = gameResult.Score;
            _context.SaveChanges();

            return Task.FromResult(true);
        }

        public Task<bool> DeleteGameResult(GameResult gameResult)
        {
            var findResult = _context.GameResults
                .Where(x => x.Id == gameResult.Id)
                .FirstOrDefault();

            if (findResult == null)
            {
                return Task.FromResult(false);
            }

            _context.GameResults.Remove(findResult);
            _context.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
