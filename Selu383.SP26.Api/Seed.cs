
using Microsoft.EntityFrameworkCore;
using Selu383.SP26.Api.Data;
using Selu383.SP26.Api.Entities;
namespace Selu383.SP26.Api
{
    public class Seed
    {
        internal static void Initialize(DataContext dbContext)
        {
            dbContext.Database.Migrate();

            if (!dbContext.Locations.Any())
            {
                var locations = new Location[]
                {
                    new Location { Name = "CoffeePlace1", Address = "123 W University Ave", TableCount = 26 },
                    new Location { Name = "CoffeePlace2", Address = "456 Palace Dr", TableCount = 30 },
                    new Location { Name = "CoffeePlace3", Address = "789 S Range Rd", TableCount = 67 }
                };

                dbContext.Locations.AddRange(locations);
                dbContext.SaveChanges();
            }
        }
    }
}