using FitnessSystem.Application.DTOs.MembershipPackage;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var membershipPackages =  await _membershipPackageService.GetAllAsync();
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

            try
            {
                var updatedMembershipPackage = await _membershipPackageService.UpdateMembershipPackageAsync(id, membershipPackageUpdateDto);
                return Ok(updatedMembershipPackage);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
