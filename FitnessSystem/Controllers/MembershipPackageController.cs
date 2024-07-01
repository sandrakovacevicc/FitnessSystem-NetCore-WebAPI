using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
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
    }
}
