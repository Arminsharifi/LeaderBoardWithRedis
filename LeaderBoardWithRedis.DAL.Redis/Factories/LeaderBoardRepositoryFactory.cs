using LeaderBoardWithRedis.DAL.Redis.Repositores;

namespace LeaderBoardWithRedis.DAL.Redis.Factories
{
    public class LeaderBoardRepositoryFactory
    {
        public static LeaderBoardRepository CreateLeaderBoardRepository(string connectionString)
        {
            return new LeaderBoardRepository(connectionString);
        }
    }
}