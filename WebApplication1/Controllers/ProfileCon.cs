using AiComp.Application.DTOs.RequestModel;
using AiComp.Application.Interfaces.Service;
using AiComp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AiComp.Controllers
{
    [Route("api/p")]
    [ApiController]
    public class ProfileCon : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;

        public ProfileCon(IProfileService profileService, IUserService userService)
        { 
            _profileService = profileService;
            _userService = userService;
        }


        [HttpPost("createprofile")]
        public async Task<IActionResult> AddProfile([FromForm] ProfileCreateModel createModel)
        {
            var currentUser = await GetCurrentUser();
            var userProfile = await _profileService.CreateNewProfile(currentUser, createModel);
            if(userProfile.Data == null)
            {
                return BadRequest(new
                {
                    status = "Bad request",
                    message = userProfile.Message,
                    statusCode = 401
                });
            }
            return Created("", new
            {
                status = "success",
                message = "Registration Successfull",
                data = new
                {
                    profile = new
                    {
                        userProfile.Data.FirstName,
                        userProfile.Data.LastName,
                        userProfile.Data.PhoneNumber,
                        userProfile.Data.Address,
                        userProfile.Data.Gender,
                        userProfile.Data.Id,
                        userProfile.Data.ContactOfNextOfKin,
                        userProfile.Data.FullNameOfNextOfKin
                    }
                }
            });
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileUpdateRequestModel model)
        {
            var user = await GetCurrentUser();
            var profileResponse = await _profileService.UpdateNewProfile(user, model);
            if (profileResponse.Data == null)
            {
                return BadRequest(new
                {
                    status = "Bad request",
                    message = profileResponse.Message,
                    statusCode = 401
                });
            }
            return Created("", new
            {
                status = "success",
                message = "Registration Successfull",
                data = new
                {
                    profile = new
                    {
                        profileResponse.Data.FirstName,
                        profileResponse.Data.LastName,
                        profileResponse.Data.PhoneNumber,
                        profileResponse.Data.Address,
                        profileResponse.Data.Gender,
                        profileResponse.Data.Id
                    }
                }
            });
        }

        [HttpPut("profilepicture")]
        public async Task<IActionResult> ChangeProfilePciture(IFormFile profilepics)
        {
            var user = await GetCurrentUser();
            var profilePics = await _profileService.ChangeProfilePics(user, profilepics);
            if(profilePics.Status == "Unsuccessfull")
            {
                return BadRequest(new
                {
                    status = "Bad request",
                    message = profilePics.Message,
                    statusCode = 401
                });
            };
            return Ok(new
            {
                status = "success",
                message = "Registration Successfull",
                data = profilePics.Data
            });
        }

        [HttpGet("profilephoto")]
        public async Task<IActionResult> GetProfilePhoto()
        {
            var currentUser = await GetCurrentUser();
            var profilePicUrlBaseResponse = await _profileService.GetProfilePic(currentUser);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ProfilePics", profilePicUrlBaseResponse.Data!);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // Return 404 if file not found
            }
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileExtension = profilePicUrlBaseResponse.Data!.Split(".");
            return File(fileBytes, $"image/{fileExtension[1]}");
        }

        [HttpGet("p")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await GetCurrentUser();
            var profile = user.Profile;
            if(profile == null)
            {
                return NotFound(new
                {
                    status = "Not Found",
                    Message = "Profile Not found",
                    StatusCode = 401
                });
            }

            return Ok(new
            {
                status = "Successful",
                Message = "Profile found",
                Data = new
                {
                    user.Profile?.FirstName,
                    user.Profile?.LastName,
                    user.Profile?.ProfilePicture,
                    user.Profile?.Address,
                    user.Profile?.Age,
                    user.Profile?.Gender,
                    user.Profile?.Occupation,
                    user.Profile?.FullNameOfNextOfKin,
                    user.Profile?.ContactOfNextOfKin,
                }
            });
        }

        [HttpGet("/p/{userId}")]
        public async Task<IActionResult> GetProfile([FromBody] Guid userId)
        {
            var profile = _profileService.GetProfile(userId);
            if (profile == null)
            {
                return NotFound(new
                {
                    status = "Not Found",
                    Message = "Profile Not found",
                    StatusCode = 401
                });
            }

            return Ok(new
            {
                status = "Successful",
                Message = "Profile found",
                Data = profile
            });
        }

        private async Task<User> GetCurrentUser()
        {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var currentUser = await _userService.GetUserByIdAsync(currentUserId);
            return currentUser;
        }  


    }
}
