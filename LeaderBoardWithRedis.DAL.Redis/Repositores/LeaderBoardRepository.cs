using LeaderBoardWithRedis.Domain.DataTransferObjects;
using LeaderBoardWithRedis.Domain.Interfaces;
using StackExchange.Redis;

namespace LeaderBoardWithRedis.DAL.Redis.Repositores
{
    public class LeaderBoardRepository : ILeaderBoardRepository
    {
        private readonly IDatabase _redisDb;

        public LeaderBoardRepository(string connectionString)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);
            _redisDb = redis.GetDatabase();
        }

        public async Task SetRecord(string stat, int score, string userName)
        {
            await _redisDb.SortedSetAddAsync(stat.ToLower(), userName.ToLower(), score);
        }

        public async Task<HashSet<UserScoreDto>> GetTopAsync(string stat, int count)
        {
            SortedSetEntry[]? results = await _redisDb.SortedSetRangeByRankWithScoresAsync(stat.ToLower(), 0, count - 1, Order.Descending);
            HashSet<UserScoreDto> topScores = new HashSet<UserScoreDto>();
            foreach (SortedSetEntry result in results)
            {
                topScores.Add(new UserScoreDto { Username = result.Element, Score = (int)result.Score });
            }
            return topScores;
        }
    }
}