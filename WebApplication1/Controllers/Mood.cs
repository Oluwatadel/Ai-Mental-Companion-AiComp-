using AiComp.Application.DTOs.RequestModel;
using AiComp.Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Embedding;

namespace AiComp.Controllers
{
    [Route("api/")]
    [ApiController]
    public class Mood : ControllerBase
    {
        private readonly IMoodService _moodService;
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;

        public Mood(IMoodService moodService, IUserService userService, IIdentityService identityService)
        {
            _moodService = moodService;
            _userService = userService;
            _identityService = identityService;
        }

        [HttpGet("moods")]
        public async Task<IActionResult> GetAllMoodLogs()
        {
            try
            {
                var currentUser = await _identityService.GetCurrentUser();
                if (currentUser == null)
                {
                    return Unauthorized();
                }
                var moodLogs = await _moodService.ViewMoodLogs(currentUser);
                if(!moodLogs.Any())
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Status = "Successful",
                    Message = $"{moodLogs.Count} mood found",
                    Data = moodLogs
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("moods/weekly")]
        public async Task<IActionResult> GetMoodLogsForAWeek()
        {
            try
            {
                var currentUser = await _identityService.GetCurrentUser();
                var today = DateTime.UtcNow;
                var weeklyMoodLog = await _moodService.ViewMoodLogsByTime(currentUser, today.AddDays(-7), today);
                if(!weeklyMoodLog.Any())
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Status = "Success",
                    Message = "MoodLogs found",
                    Data = weeklyMoodLog
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("mood/Search")]
        public async Task<IActionResult> GetMoodLogsForDaysSpecifiedByUser([FromBody] MoodSearchRequest request)
        {
            try
            {
                var currentUser = await _identityService.GetCurrentUser();
                var logsBasedOnUserSearch = await _moodService.ViewMoodLogsByTime(currentUser, request.StartDate, request.EndDate);
                if(!logsBasedOnUserSearch.Any())
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Status = "Success",
                    Message = "Mood(s) found",
                    Data = logsBasedOnUserSearch
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
