using FitnessSystem.Application.DTOs.MembershipPackage;
using FitnessSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessSystem.Presentation.Controllers
{
    [Route("api/membership-packages")]
    [ApiController]
    public class MembershipPackageController : ControllerBase
    {
        private readonly IMembershipPackageService _membershipPackageService;

        public MembershipPackageController(IMembershipPackageService membershipPackageService)
        {
            _membershipPackageService = membershipPackageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MembershipPackageDto>>> GetAll()
        {
            var membershipPackages = await _membershipPackageService.GetAllAsync();
            return Ok(membershipPackages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var membershipPackageDto = await _membershipPackageService.GetByIdAsync(id);
            if (membershipPackageDto == null)
            {
                return NotFound();
            }
            return Ok(membershipPackageDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MembershipPackageUpdateDto>> UpdateMembershipPackage(int id, [FromBody] MembershipPackageUpdateDto membershipPackageUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedMembershipPackage = await _membershipPackageService.UpdateMembershipPackageAsync(id, membershipPackageUpdateDto);
            return Ok(updatedMembershipPackage);
        }
    }
}
