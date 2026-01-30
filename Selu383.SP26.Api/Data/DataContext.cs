
using Microsoft.EntityFrameworkCore;
using Selu383.SP26.Api.Entities;
using System.Reflection;

namespace Selu383.SP26.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Locations> Locations { get; set; }
    }
}