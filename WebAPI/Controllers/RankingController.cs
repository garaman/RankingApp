using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedData.Models;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    // REST
    // 공식 표준 스펙은 아님
    // 원래 있던 HTTP 통신에서 기능을 '재사용'해서데이터 송수신 규칙을 만든것.


    // CRLD
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        ApplicationDbContext _context;

        public RankingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create

        // Read
        [HttpGet]
        public List<GameResult> GetGameResults()
        {
            List<GameResult> results = _context.GameResults
                .OrderByDescending(item => item.Score)
                .ToList();

            return results;
        }

        [HttpGet("{id}")]
        public GameResult GetGameResults(int id)
        {
            GameResult? result = _context.GameResults
                .Where(item => item.Id == id)
                .FirstOrDefault();

            return result;
        }

        // Update

        // Delete
    }
}
