using Newtonsoft.Json;
using SharedData.Models;
using System.Text;

namespace RankingApp.Data.Service
{
    public class RankingService
    {
        HttpClient _httpClient;

        public RankingService(HttpClient client)
        {
            _httpClient = client;
        }

        // Create
        public async Task<GameResult> AddGameResult(GameResult gameResult)
        {
            string jsonStr = JsonConvert.SerializeObject(gameResult);
            var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/Ranking", content);

            if (result.IsSuccessStatusCode == false)
            {
                throw new Exception("AddGameResult Failed");
            }

            var resultContent = await result.Content.ReadAsStringAsync();
            GameResult resGameResult = JsonConvert.DeserializeObject<GameResult>(resultContent);
            return resGameResult;
        }

        // Read
        public async Task<List<GameResult>> GetGameResultsAsync()
        {
            var result = await _httpClient.GetAsync("api/Ranking");

            var resultContent = await result.Content.ReadAsStringAsync();
            List<GameResult> resGameResults = JsonConvert.DeserializeObject<List<GameResult>>(resultContent);
            return resGameResults;
        }

        // Update
        public async Task<bool> UpdateGameResult(GameResult gameResult)
        {
            string jsonStr = JsonConvert.SerializeObject(gameResult);
            var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync("api/Ranking", content);

            if (result.IsSuccessStatusCode == false)
            {
                throw new Exception("UpdateGameResult Failed");                
            }

            return true;
        }

        public async Task<bool> DeleteGameResult(GameResult gameResult)
        {
            var result = await _httpClient.DeleteAsync($"apu/Ranking/{gameResult.Id}");

            if (result.IsSuccessStatusCode == false)
            {
                throw new Exception("DeleteGameResult Failed");
            }

            return true;
        }
    }
}
