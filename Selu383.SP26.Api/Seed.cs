
using Microsoft.EntityFrameworkCore;
using Selu383.SP26.Api.Data;
using Selu383.SP26.Api.Entities;
namespace Selu383.SP26.Api
{
    public class SeedLocationsInitial
    {
        public static async Task Initialize(DataContext dbContext)
        {
            if (!dbContext.Locations.Any())
            {
                await SeedLocations(dbContext);
            }
        }

        private static async Task SeedLocations(DataContext dbContext)
        {
            //dbContext.Database.Migrate(); this had to be commented out after the initial migration, probably won't work if you uncomment

                var locations = new Location[]
                {
                    new Location { Name = "CoffeePlace1", Address = "123 W University Ave", TableCount = 26 },
                    new Location { Name = "CoffeePlace2", Address = "456 Palace Dr", TableCount = 30 },
                    new Location { Name = "CoffeePlace3", Address = "789 S Range Rd", TableCount = 67 }
                };

                dbContext.Locations.AddRange(locations);
                await dbContext.SaveChangesAsync();
            }
        }
    }