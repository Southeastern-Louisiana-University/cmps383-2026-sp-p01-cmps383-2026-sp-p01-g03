using System.ComponentModel.DataAnnotations;
namespace Selu383.SP26.Api.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int TableCount { get; set; }
    }

    public class LocationGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int TableCount { get; set; }
    }

    public class LocationCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int TableCount { get; set; }
    }

    public class LocationUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int TableCount { get; set; }
    }
}