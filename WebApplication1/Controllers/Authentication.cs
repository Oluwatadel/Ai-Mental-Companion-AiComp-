﻿using AiComp.Application.DTOs.RequestModel;
using AiComp.Application.Interfaces.Service;
using AiComp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AiComp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class Authentication : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;

        public Authentication(IUserService userService, IIdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestModel request)
        {
            var user = await _userService.UserExist(request.Email);
            if (user == true) return Created("duplicate email", new
            {
                status = "Duplicate email",
                message = "Duplicate Email",
                statusCode = 400
            });
            var newUser = await _userService.AddUserAsync(request);
            return Created("", new
            {
                status = "success",
                message = "Registration Successfull",
                data = new
                {
                    newUser.Id,
                    newUser.Email,
                }
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var userExist = await _userService.UserExist(request.Email);
            if (!userExist) return NotFound(new
            {
                status = "Not Found",
                message = "User does not exist",
                statusCode = 404
            });
            var passWordCheck = await _identityService.AuntenticateUser(request.Email, request.Password);
            if(!passWordCheck)
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserAsync(request.Email);
            var token = _identityService.GenerateToken(user);
            if (user.Profile == null) return Ok(new
            {
                status = "Successfull",
                message = "Login successfull",
                data = new
                {
                    accessToken = token,
                    user = new
                    {
                        user.Id,
                        user.Email,
                    },
                },
                user.Profile,

            });


            return Ok(new
            {
                status = "Successfull",
                message = "Login successfull",
                data = new
                {
                    accessToken = token,
                    user = new
                    {
                        user.Id,
                        user.Email,
                    },
                },
                profile = true
                //profile = new
                //{
                //    user.Profile.FirstName,
                //    user.Profile.LastName,
                //    user.Profile.FullNameOfNextOfKin,
                //    user.Profile.ContactOfNextOfKin,
                //    user.Profile.Age,
                //    user.Profile.Gender,
                //    user.Profile.ProfilePicture,
                //    user.Profile.Occupation,
                //    user.Profile.Address,
                //}
            });
        }

        [HttpPut("email/update")]
        public async Task<IActionResult> UpdateEmail([FromBody] string email)
        {
            User user = null;

            //User not logged In
            try
            {
                user = await _identityService.GetCurrentUser();/*User.FindFirst(ClaimTypes.NameIdentifier)?.Value*/;
            }
            catch(Exception e)
            {
                return BadRequest(new
                {
                    message = "No user is logged in",
                    statusCode = 404,
                    status = "Unsuccessfull",

                });
            }
            var newUser = await _userService.UpdateUserEmailAsync(user.Id, email);

            //Internal server error
            if(newUser == null)
            {
                return BadRequest(new
                {
                    message = "Something went wrong",
                    statusCode = 404,
                    status = "Unsuccessfull",

                });
            }

            //Successfull update
            return Ok(new
            {
                message = "Email updated succesfully",
                statusCode = 200,
                status = "Successfull"
            });
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if(users.Count == 0)
            {
                return Ok(new
                {
                    message = "Users yet to register",
                    status = "No users found",
                    statusCode = 200,

                });
            }

            return Created("", new
            {
                message = $"{users.Count} users yet to register",
                status = "Successful",
                statusCode = 200,
                Data = users.Select(p => new
                {
                    email = p.Email,
                    isConsented = p.IsConsented,
                    
                })
            });
        }

        

        //[HttpPost("LogOut")]
        //public async Task<IActionResult> LogOut()
        //{
        //    var currentUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (string.IsNullOrEmpty(currentUser)) return
        //            Unauthorized(new
        //            {
        //                status = "Unauthorised",
        //                Message = "User is Unauthorised",
        //                StatusCode = 401
        //            });
        //    var invalidateToken = 
        //}
    }
}