using AiComp.Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AiComp.Controllers
{
    [Route("api/")]
    [ApiController]
    public class Mood : ControllerBase
    {
        private readonly IMoodService _moodService;
        private readonly IUserService _userService;

        public Mood(IMoodService moodService, IUserService userService)
        {
            _moodService = moodService;
            _userService = userService;
        }

        //public Task<IActionResult> AddMoodLog()
    }
}
