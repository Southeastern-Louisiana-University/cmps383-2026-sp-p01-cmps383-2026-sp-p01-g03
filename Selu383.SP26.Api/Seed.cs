
using Microsoft.EntityFrameworkCore;
using Selu383.SP26.Api.Data;
using Selu383.SP26.Api.Entities;


public class LocationSeeder
{
    public static async Task Initialize(DataContext context)
    {
        if (!context.Locations.Any())
        {
            await SeedLocations(context);
        }
    }

    private static async Task SeedLocations(DataContext context)
    {
        var seededLocations = new List<Locations>()
        {
            new Locations
            {
                Name = "CoffeePlace1",
                Address = "30 Epic St, Baton Rouge",
                TableCount = 20
            },
            new Locations
            {
                Name = "CoffeePlace2",
                Address = "5397 Main St, Hammond",
                TableCount = 40
            },
            new Locations
            {
                Name = "CoffeePlace3",
                Address = "1 Cool Dr, Plaquemine",
                TableCount = 67
            }
        };

        context.Locations.AddRange(seededLocations);
        await context.SaveChangesAsync();
    }
}