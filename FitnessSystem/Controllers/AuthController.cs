﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Application.DTOs.Login;
using System.IdentityModel.Tokens.Jwt;

namespace Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthentificationService _tokenService;

        public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAuthentificationService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var user = new Client
            {
                JMBG = registerRequestDto.JMBG,
                Name = registerRequestDto.Name,
                Surname = registerRequestDto.Surname,
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.JMBG,
                Birthdate = registerRequestDto.Birthdate,
                MobileNumber = registerRequestDto.MobileNumber,
                MembershipPackageId = registerRequestDto.MembershipPackageId,
                IsPaid = registerRequestDto.IsPaid,

            };

            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
            user.Id = user.JMBG;

            if (result.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    foreach (var roleName in registerRequestDto.Roles)
                    { 
                        var response = await _userManager.AddToRoleAsync(user, "Client");
                    }
                }

                return Ok("User registered successfully! Please login.");
            }

            return BadRequest("Failed to register user. Please check the provided details.");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);
            if (user != null)
            {
                var checkPass = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPass)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = _tokenService.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }

                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("decode-token")]
        public IActionResult DecodeToken([FromBody] string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var claims = jwtToken.Claims.Select(c => new { c.Type, c.Value }).ToList();

            return Ok(claims);
        }
    }
}

