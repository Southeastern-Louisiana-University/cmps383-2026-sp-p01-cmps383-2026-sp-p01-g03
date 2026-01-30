using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Selu383.SP26.Api.Data;
using Selu383.SP26.Api.Entities;

namespace Selu383.SP26.Api.Controllers
{
    [ApiController]
    [Route("/locations")]
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;
        private readonly DataContext _dataContext;

        public LocationsController(ILogger<LocationsController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }


        [HttpGet(Name = "List All")]
        public ActionResult<List<Locations>> GetLocations()
        {
            var location = _dataContext.Locations.ToList();
            return Ok(location);
        }


    }
}