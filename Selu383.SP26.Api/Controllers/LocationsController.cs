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
        static private List<Location> Location = new List<Location>
        {
            new Location {
                Id = 1,
                Name = "test1",
                Address = "address1",
                TableCount = 10,
            },
            new Location {
                Id = 1,
                Name = "test2",
                Address = "address2",
                TableCount = 20,
            },
        };
        private readonly ILogger<LocationsController> _logger;
        private readonly DataContext _dataContext;

        public LocationsController(ILogger<LocationsController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }


        [HttpGet(Name = "List All")]
        public IEnumerable<Location> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Location
            {
                Name = "test",
                Address = "addresstest",
                TableCount = 1
            })
                .ToArray();

        }
    }
}