using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using SpaceshipService.Context;
using SpaceshipService.Models;
using SpaceshipService.Models.DTO;

namespace SpaceshipService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]

    public class SpaceshipController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public SpaceshipController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetSpaceship()
        {
            return Ok(new ApiResponse<List<Spaceship>>(true, await _dbContext.Spaceships.ToListAsync()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpaceship(Spaceship spaceship)
        {
            if (spaceship == default)
            {
                return BadRequest(new ApiResponse<string>(false, "Object cannot be null!"));
            }

            if (string.IsNullOrEmpty(spaceship.ShipName))
            {
                return BadRequest(new ApiResponse<string>(false, "Spaceship name cannot be empty!"));
            }

            if (await _dbContext.Spaceships.AnyAsync(x => x.ShipName == spaceship.ShipName))
            {
                return BadRequest(new ApiResponse<string>(false, "Spaceship name already exists!"));
            }

            await _dbContext.AddAsync(spaceship);
            await _dbContext.SaveChangesAsync();

            return Ok(new ApiResponse<Spaceship>(true, spaceship));
        }

        [HttpGet("getSpaceship/{ManufacturerId}")]
        public async Task<IActionResult> GetSpaceshipByManuId(int ManufacturerId)
        {
            return Ok(new ApiResponse<List<Spaceship>>(true, await _dbContext.Spaceships.Where(x => x.ManufacturerID == ManufacturerId).ToListAsync()));
        }
    }
}