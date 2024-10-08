﻿using FitnessSystem.Application.DTOs.User;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessSystem.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }


        [HttpGet("{jmbg}")]
        public async Task<IActionResult> GetById(string jmbg)
        {
            var userDto = await _userService.GetByIdAsync(jmbg);
            if (userDto == null)
            {
                return NotFound();
            }
            return Ok(userDto);
        }

    }
}
