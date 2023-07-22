using LeaderBoardWithRedis.Domain.DataTransferObjects;

namespace LeaderBoardWithRedis.Domain.Interfaces
{
    public interface ILeaderBoardRepository
    {
        Task SetRecord(string stat, int score, string userName);
        Task<HashSet<UserScoreDto>> GetTopAsync(string stat, int count);
    }
}