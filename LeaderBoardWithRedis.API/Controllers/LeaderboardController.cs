using LeaderBoardWithRedis.Domain.DataTransferObjects;
using LeaderBoardWithRedis.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LeaderBoardWithRedis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;
        private readonly ILogger<LeaderboardController> _logger;

        public LeaderboardController(ILeaderBoardRepository leaderBoardRepository, ILogger<LeaderboardController> logger)
        {
            _leaderBoardRepository = leaderBoardRepository;
            _logger = logger;
        }

        [HttpGet("GetTop")]
        public async Task<IActionResult> GetTop([Required][FromQuery] string stat)
        {
            try
            {
                HashSet<UserScoreDto> userScores = await _leaderBoardRepository.GetTopAsync(stat, 20);
                return userScores.Count > 0 ? Ok(userScores) : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("SetScore")]
        public async Task<IActionResult> SetScore([Required][FromQuery] string stat, [Required][FromQuery] int score, [Required][FromQuery] string username)
        {
            try
            {
                await _leaderBoardRepository.SetRecord(stat, score, username);
                _logger.LogInformation($"A score was set with stat: '{stat}' and score: '{score}' for username: '{username}'.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}