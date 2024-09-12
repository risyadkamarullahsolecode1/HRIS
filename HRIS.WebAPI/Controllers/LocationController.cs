using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRIS.Domain.Entities;
using HRIS.Domain.Interfaces;
using HRIS.Infrastructure.Data.Repository;

namespace HRIS.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {

            _locationRepository = locationRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations()
        {
            var Location = await _locationRepository.GetAllLocations();
            return Ok(Location);
        }
        [HttpPost]
        public async Task<ActionResult<Location>> AddLocation(Location location)
        {
            var createdLocation = await _locationRepository.AddLocation(location);
            return Ok(createdLocation);
        }
        [HttpPut]
        public async Task<ActionResult<Location>> UpdateLocation(Location location)
        {
            var updatedLocation = await _locationRepository.UpdateLocation(location);
            return Ok(updatedLocation);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteLocation(Location location)
        {
            await _locationRepository.DeleteLocation(location);
            return Ok("location has been deleted");
        }
    }
}
