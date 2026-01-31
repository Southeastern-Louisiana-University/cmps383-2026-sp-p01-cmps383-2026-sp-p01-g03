using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Selu383.SP26.Api.Data;
using Selu383.SP26.Api.Entities;

namespace Selu383.SP26.Api.Controllers
{
    [ApiController]
    [Route("/api/locations")]
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;
        private readonly DataContext _dataContext;

        public LocationsController(ILogger<LocationsController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult ListAllLocations()
        {
            var locations = _dataContext.Locations
                .Select(l => new LocationGetDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    Address = l.Address,
                    TableCount = l.TableCount
                })
                .ToList();

            return Ok(locations);
        }

        [HttpGet("{id}")]
        public ActionResult<Location> GetLocationById(int id)
        {
            var location = _dataContext.Locations.FirstOrDefault(l => l.Id == id);
            if (location is null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        [HttpPost]
        public ActionResult<LocationGetDto> CreateLocation(LocationCreateDto newLocationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Dto -> Entity
            var newLocation = new Location
            {
                Name = newLocationDto.Name,
                Address = newLocationDto.Address,
                TableCount = newLocationDto.TableCount
            };

            if (string.IsNullOrEmpty(newLocation.Address))
            {
                return NotFound(ModelState);
            }

            _dataContext.Locations.Add(newLocation);
            _dataContext.SaveChanges();

            // Entity -> DTO
            var locationDto = new LocationGetDto
            {
                Id = newLocation.Id,
                Name = newLocation.Name,
                Address = newLocation.Address,
                TableCount = newLocation.TableCount
            };

            return CreatedAtAction(
                nameof(GetLocationById),
                new { id = locationDto.Id },
                locationDto
            );
        }

        [HttpPut("{id}")]
        public ActionResult<LocationGetDto> UpdateLocationById(int id, LocationUpdateDto updateLocationDto)
        {
            var existingLocation = _dataContext.Locations.FirstOrDefault(l => l.Id == id);

            if (existingLocation == null)
            {
                return NotFound();
            }

            // Update the existing entity with new values from the DTO
            existingLocation.Name = updateLocationDto.Name;
            existingLocation.Address = updateLocationDto.Address;
            existingLocation.TableCount = updateLocationDto.TableCount;

            // Save the changes
            _dataContext.SaveChanges();

            // Entity -> DTO mapping
            var locationDto = new LocationGetDto
            {
                Id = existingLocation.Id,
                Name = existingLocation.Name,
                Address = existingLocation.Address,
                TableCount = existingLocation.TableCount
            };

            return Ok(locationDto);
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteLocation(int id)
        {
            var location = _dataContext.Locations.FirstOrDefault(x => x.Id == id);
            if (location is null)
            {
                return NotFound();
            }
            _dataContext.Locations.Remove(location);
            _dataContext.SaveChanges();
            return Ok();
        }

    }
}