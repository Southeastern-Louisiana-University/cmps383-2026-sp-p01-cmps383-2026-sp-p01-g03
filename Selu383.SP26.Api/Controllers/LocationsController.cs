using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // Had to change Location entity to LocationGetDto so it matches the format returned by the list endpoint
        public ActionResult<LocationGetDto> GetLocationById(int id)
        {
            var location = _dataContext.Locations.FirstOrDefault(l => l.Id == id);
            if (location is null)
            {
                return NotFound();
            }
            var locationDto = new LocationGetDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                TableCount = location.TableCount
            };
            return Ok(locationDto);
        }

        [HttpPost]
        public ActionResult<LocationGetDto> CreateLocation(LocationCreateDto newLocationDto)
        {
            // All four of these should replace the old sketchy validation in the Locations class, should return 400
            if (string.IsNullOrEmpty(newLocationDto.Name))
            {
                return BadRequest("Name is required.");
            }

            if (newLocationDto.Name.Length > 120)
            {
                return BadRequest("Name must be 120 characters or fewer.");
            }

            if (string.IsNullOrEmpty(newLocationDto.Address))
            {
                return BadRequest("Address is required.");
            }

            if (newLocationDto.TableCount < 1)
            {
                return BadRequest("TableCount must be at least 1.");
            }

            // Dto -> Entity
            var newLocation = new Location
            {
                Name = newLocationDto.Name,
                Address = newLocationDto.Address,
                TableCount = newLocationDto.TableCount
            };

            _dataContext.Locations.Add(newLocation);
            _dataContext.SaveChanges();

            // Entity -> Dto
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
            // Same validation logic as above
            if (string.IsNullOrEmpty(updateLocationDto.Name))
            {
                return BadRequest("Name is required.");
            }

            if (updateLocationDto.Name.Length > 120)
            {
                return BadRequest("Name must be 120 characters or fewer.");
            }

            if (string.IsNullOrEmpty(updateLocationDto.Address))
            {
                return BadRequest("Address is required.");
            }

            var existingLocation = _dataContext.Locations.FirstOrDefault(l => l.Id == id);

            if (existingLocation == null)
            {
                return NotFound();
            }

            // Update the existing entity with new values from the Dto
            existingLocation.Name = updateLocationDto.Name;
            existingLocation.Address = updateLocationDto.Address;
            existingLocation.TableCount = updateLocationDto.TableCount;

            _dataContext.SaveChanges();

            // Entity -> Dto
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
