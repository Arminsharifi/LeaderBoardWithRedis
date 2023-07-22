namespace LeaderBoardWithRedis.Domain.DataTransferObjects
{
    public class UserScoreDto
    {
        public string Username { get; set; }
        public int Score { get; set; }
    }
}