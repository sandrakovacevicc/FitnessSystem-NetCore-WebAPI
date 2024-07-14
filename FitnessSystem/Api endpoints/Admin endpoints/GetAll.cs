using Ardalis.ApiEndpoints;
using Core.Entities;
using FitnessSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessSystem.Presentation.Api_endpoints.Admin_endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<IEnumerable<Admin>>
    {
        private IAdminService _adminService;

        public GetAll(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("api/v1/admins")]
        public async override Task<ActionResult<IEnumerable<Admin>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var admins = await _adminService.GetAllAsync();
            return Ok(admins);
        }
    }
}
